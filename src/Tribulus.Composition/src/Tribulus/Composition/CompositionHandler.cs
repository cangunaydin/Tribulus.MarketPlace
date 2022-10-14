using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tribulus.Composition.ModelBinding;
using Volo.Abp;
using Volo.Abp.Guids;
using Volo.Abp.Reflection;

namespace Tribulus.Composition;

public class CompositionHandler
{
    internal async Task<object> HandleComposableRequest(HttpContext context,
        CompositionRouteRegistry compositionRouteRegistry)
    {

        var serviceProvider = context.RequestServices;

        var compositionContext = serviceProvider.GetService<ICompositionContext>();
        var guidGenerator = serviceProvider.GetService<IGuidGenerator>();

        context.Request.EnableBuffering();
        var httpRequest = context.Request;
        var routeData = context.GetRouteData();
        var queryStrings = context.Request.Query;


        object input = null;
        //check route values first.

        List<object> parameterList = await CreateParameters(context, compositionRouteRegistry);
        
        var requestId= guidGenerator.Create();
        object output = null;
        if (compositionRouteRegistry.ViewModelType!=null)
        {
            output = Activator.CreateInstance(compositionRouteRegistry.ViewModelType);
            var outputBase = (CompositionViewModelBase)output;
            outputBase.RequestId = requestId;
        }
       

       
        compositionContext.HttpRequest = httpRequest;
        compositionContext.RequestId = requestId.ToString();
        compositionContext.HttpRequest.SetViewModel(output);

        try
        {

            foreach (var subscribe in compositionRouteRegistry.Subscribers)
            {
                var subscribeService = serviceProvider.GetServices<ICompositionSubscribeService>().Where(o => o.GetType() == subscribe.ComponentType).First();
                if (parameterList.Any())
                    subscribe.MethodInfo.Invoke(subscribeService, parameterList.ToArray());
                else
                    subscribe.MethodInfo.Invoke(subscribeService, null);
            }


            var handlers = compositionRouteRegistry.Handlers
                    .Select(registry => registry.MethodInfo)
                    .ToList();
            if (!compositionRouteRegistry.Handlers.Any())
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return null;

            }
            else
            {

                List<(Task,MethodInfo)> handlerTasks = new List<(Task, MethodInfo)>();
                foreach (var handler in compositionRouteRegistry.Handlers)
                {
                    var handlerService = serviceProvider.GetServices<ICompositionHandleService>().Where(o => o.GetType() == handler.ComponentType).First();
                    Task handlerTask;
                    if (parameterList.Any())
                        handlerTask = (Task)handler.MethodInfo.Invoke(handlerService, parameterList.ToArray());
                    else
                        handlerTask = (Task)handler.MethodInfo.Invoke(handlerService, null);
                    handlerTasks.Add((handlerTask, handler.MethodInfo));

                }

                try
                {
                    await Task.WhenAll(handlerTasks.Select(o=>o.Item1));
                }
                catch (System.Exception ex)
                {
                    //TODO: refactor to Task.WhenAll
                    var errorHandlers = handlers.OfType<ICompositionErrorsHandler>();
                    foreach (var handler in errorHandlers)
                    {
                        await handler.OnRequestError(httpRequest, ex);
                    }

                    throw;
                }

                foreach (var taskMethodInfo in handlerTasks)
                {
                    object value = null;
                    var task = taskMethodInfo.Item1;
                    var methodInfo= taskMethodInfo.Item2;
                    if (task.GetType().IsGenericType)
                    {
                        var resultProperty = task.GetType().GetProperty("Result");
                        value = resultProperty.GetValue(task);
                        if (handlerTasks.Count == 1)
                        {
                            output = value;
                            return output;
                        }
                    }
                    
                    var viewModelProperty = (ViewModelPropertyAttribute)methodInfo.GetCustomAttribute(typeof(ViewModelPropertyAttribute));
                    string viewModelPropertyName = viewModelProperty != null ? viewModelProperty.PropertyName : null;
                    
                   
                    if (!string.IsNullOrEmpty(viewModelPropertyName))
                    {
                        Type type = value.GetType();
                        PropertyInfo info = type.GetProperty(viewModelPropertyName);

                        value = info.GetValue(value);

                        var propertyToUpdate = compositionRouteRegistry
                        .ViewModelType
                        .GetProperties(
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public |
                        BindingFlags.Instance)
                        .Where(o => o.Name.ToLowerInvariant() == viewModelPropertyName.ToLowerInvariant()).FirstOrDefault();
                        if (propertyToUpdate == null)
                            throw new Exception($"Property name {viewModelPropertyName} can not found in the viewmodel.");

                        propertyToUpdate.SetValue(output, value);
                    }
                }
                return output;

            }

        }
        finally
        {
            compositionContext.CleanupSubscribers();
        }

    }
    private async Task<List<object>> CreateParameters(HttpContext context, CompositionRouteRegistry compositionRouteRegistry)
    {
        var routeData = context.GetRouteData();
        var methodParameters = compositionRouteRegistry.Handlers
                             .SelectMany(o => o.MethodInfo.GetParameters().Select(o => new { o.Name, o.ParameterType }))
                             .Distinct()
                             .Where(o => !string.IsNullOrEmpty(o.Name))
                             .ToDictionary(x => x.Name, x => x.ParameterType);
        

        var parameterList = new List<object>();
        foreach (var routeValue in routeData.Values)
        {
            var parameter = GetParameter(routeValue.Key, routeValue.Value, methodParameters);
            if (parameter != null)
                parameterList.Add(parameter);
        }
        var complexType = compositionRouteRegistry.InputTypes.FirstOrDefault(o => !TypeHelper.IsPrimitiveExtended(o));

        if (complexType!=null)
        {
            var complexValue = await context.Request.Bind(complexType);
            parameterList.Add(complexValue);
        }
        return parameterList;
    }
    private object GetParameter(string key, object value, Dictionary<string, Type> methodParameters)
    {
        object parameter = null;
        if (methodParameters.IsNullOrEmpty())
            return null;
        var methodParameter = methodParameters.Where(o => o.Key == key).FirstOrDefault();

        if (value != null && methodParameter.Value!=null)
        {
            try
            {
                parameter = TypeDescriptor.GetConverter(methodParameter.Value).ConvertFrom(value);
            }
            catch (InvalidCastException ex)
            {
                throw new Exception($"Can not cast value {value} to {methodParameter.Value}");
            }

        }
        return parameter;
    }

}