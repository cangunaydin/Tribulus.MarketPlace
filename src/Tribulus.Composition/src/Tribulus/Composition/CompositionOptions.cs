using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http;
using Volo.Abp.Http.Modeling;

namespace Tribulus.Composition.Tribulus.Composition
{
    public class CompositionOptions : ISingletonDependency
    {
        private List<CompositionRouteRegistry> _compositionRouteRegistry;
        private bool _isRegistered=false;
        private readonly IConventionalRouteBuilder _conventionalRouteBuilder;
        

        protected AbpAspNetCoreMvcOptions _options { get; }

        public List<CompositionRouteRegistry> CompositionRouteRegistry { get => _compositionRouteRegistry; }
        public bool IsRegistered { get => _isRegistered; }

        public CompositionOptions(IConventionalRouteBuilder conventionalRouteBuilder, IOptions<AbpAspNetCoreMvcOptions> options)
        {
            _compositionRouteRegistry = new List<CompositionRouteRegistry>();
            _conventionalRouteBuilder = conventionalRouteBuilder;
            _options = options.Value;
        }

        internal void CreateCompositionRoutes(
            IEnumerable<ICompositionHandleService> compositionHandlers,
            IEnumerable<ICompositionSubscribeService> compositionSubscribers)
        {
            _compositionRouteRegistry = new List<CompositionRouteRegistry>();

            //create composition route items.
            List<CompositionRouteItem> compositionRouteItems = new List<CompositionRouteItem>();
            foreach (var compositionHandleService in compositionHandlers)
            {
                var compositionClassType = compositionHandleService.GetType();
                var compositionClassInfo = compositionHandleService.GetType().GetTypeInfo();
                foreach (var methodInfo in compositionClassInfo.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                {
                    if (methodInfo.ReturnType != typeof(Task) && methodInfo.ReturnType.BaseType != typeof(Task))
                        throw new Exception("Return type should be Task for handlers.");

                    var httpMethod = SelectHttpMethod(methodInfo);
                    var inputs = methodInfo.GetParameters().ToList();
                    //var actionModel = new ActionModel(methodInfo, (IReadOnlyList<object>)methodInfo.GetCustomAttributes());

                    var route = _conventionalRouteBuilder.Build(ModuleApiDescriptionModel.DefaultRootPath, compositionClassInfo.Name.RemovePostFix(ApplicationService.CommonPostfixes), ConvertMethodInfoToActionModel(methodInfo), httpMethod, null);
                    //ViewModelPropertyAttribute? viewModelAttribute = (ViewModelPropertyAttribute?)methodInfo.GetCustomAttribute(typeof(ViewModelPropertyAttribute));
                    //Type? viewModelType = viewModelAttribute != null ?  viewModelAttribute.ViewModelType : null;
                    Type? viewModelType = methodInfo.ReturnType.GenericTypeArguments.Any() ? methodInfo.ReturnType.GenericTypeArguments[0] : null;

                    compositionRouteItems.Add(new CompositionRouteItem()
                    {
                        Route = route,
                        HttpMethod = httpMethod,
                        ComponentType = compositionClassType,
                        MethodInfo = methodInfo,
                        ViewModelType = viewModelType,
                        InputTypes = inputs.Select(o=>o.ParameterType).ToList()
                    });
                }
            }
            //create composition subscriptions.
            foreach (var compositionSubscribe in compositionSubscribers)
            {
                var compositionClassType = compositionSubscribe.GetType();

                var compositionClassInfo = compositionSubscribe.GetType().GetTypeInfo();
                foreach (var methodInfo in compositionClassInfo.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                {
                    var httpMethod = SelectHttpMethod(methodInfo);
                    var inputs = methodInfo.GetParameters().ToList();
                    
                    var route = _conventionalRouteBuilder.Build(ModuleApiDescriptionModel.DefaultRootPath, compositionClassInfo.Name.RemovePostFix(ApplicationService.CommonPostfixes), ConvertMethodInfoToActionModel(methodInfo), httpMethod, null);
                    if (methodInfo.ReturnType != typeof(Task) && methodInfo.ReturnType != typeof(void))
                    {
                        throw new Exception("Subscribers should have return type as void.");
                    }
                    compositionRouteItems.Add(new CompositionRouteItem()
                    {
                        Route = route,
                        HttpMethod = httpMethod,
                        ComponentType = compositionClassType,
                        MethodInfo = methodInfo,
                        InputTypes = inputs.Select(o => o.ParameterType).ToList()
                    });
                }
            }

            //Create compositionRegistry
            var componentsGroupedByRoute = compositionRouteItems.GroupBy(o => new { o.Route, o.HttpMethod }).ToList();
            foreach (var components in componentsGroupedByRoute)
            {
                //handlers
                var httpMethod = ConvertToHttpMethod(components.Key.HttpMethod);
                var handlers = components.Where(o => !o.IsSubscriber).ToList();
                //check if handlers have only one viewmodel type.
                if (handlers.Select(o => o.ViewModelType).Distinct().Count() > 1)
                    throw new Exception("handlers can only have 1 viewmodel");


                var viewModelType = handlers.Select(o => o.ViewModelType).FirstOrDefault();

                var inputTypes = handlers.Select(o => o.InputTypes).FirstOrDefault();
                //subscribers
                var subscribers = components.Where(o => o.IsSubscriber).ToList();
                var (controllerSetting,componentType,methodInfo) = FindControllerSettingOrNull(components.ToList());

                var newRegistry = new CompositionRouteRegistry();
                
                newRegistry.Route = components.Key.Route;
                if (controllerSetting != null)
                {
                    newRegistry.Route = _conventionalRouteBuilder.Build(GetRootPathOrDefault(componentType), componentType.GetTypeInfo().Name.RemovePostFix(ApplicationService.CommonPostfixes),
                              ConvertMethodInfoToActionModel(methodInfo), SelectHttpMethod(methodInfo), controllerSetting);
                }
                newRegistry.Method = httpMethod;
                newRegistry.ViewModelType = viewModelType;
                newRegistry.InputTypes = inputTypes;
                newRegistry.Subscribers = subscribers;
                newRegistry.Handlers = handlers;
                _compositionRouteRegistry.Add(newRegistry);
            }

            _isRegistered = true;
        }
        protected virtual string GetRootPathOrDefault(Type controllerType)
        {
            var controllerSetting = GetControllerSettingOrNull(controllerType);
            if (controllerSetting?.RootPath != null)
            {
                return controllerSetting.RootPath;
            }

            var areaAttribute = controllerType.GetCustomAttributes().OfType<AreaAttribute>().FirstOrDefault();
            if (areaAttribute?.RouteValue != null)
            {
                return areaAttribute.RouteValue;
            }

            return ModuleApiDescriptionModel.DefaultRootPath;
        }
        [CanBeNull]
        protected virtual (ConventionalControllerSetting,Type,MethodInfo) FindControllerSettingOrNull(List<CompositionRouteItem> routeItems)
        {
            foreach (var routeItem in routeItems)
            {
                var controllerSetting = GetControllerSettingOrNull(routeItem.ComponentType);
                if (controllerSetting != null)
                    return (controllerSetting,routeItem.ComponentType,routeItem.MethodInfo);
            }
            return (null,null,null);
        }
        [CanBeNull]
        protected virtual ConventionalControllerSetting GetControllerSettingOrNull(Type controllerType)
        {
            return _options.ConventionalControllers.ConventionalControllerSettings.GetSettingOrNull(controllerType);
        }
        private static string SelectHttpMethod(MethodInfo methodInfo)
        {
            return HttpMethodHelper.GetConventionalVerbForMethodName(methodInfo.Name);
        }
        private static HttpMethod ConvertToHttpMethod(string httpMethod)
        {
            return HttpMethodHelper.ConvertToHttpMethod(httpMethod);
        }
        private ActionModel ConvertMethodInfoToActionModel(MethodInfo methodInfo)
        {
            var actionModel = new ActionModel(methodInfo, (IReadOnlyList<object>)methodInfo.GetCustomAttributes());
            actionModel.ActionName = methodInfo.Name;
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                var parameterModel = new ParameterModel(parameterInfo, (IReadOnlyList<object>)parameterInfo.GetCustomAttributes());
                parameterModel.ParameterName = parameterInfo.Name;
                actionModel.Parameters.Add(parameterModel);
            }

            return actionModel;
        }

    }   
}
