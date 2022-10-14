using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Tribulus.Composition.ModelBinding;

public class RequestModelBinder:ISingletonDependency
{
    IModelBinderFactory modelBinderFactory;
    IModelMetadataProvider modelMetadataProvider;
    IOptions<MvcOptions> mvcOptions;

    public RequestModelBinder(IModelBinderFactory modelBinderFactory, IModelMetadataProvider modelMetadataProvider, IOptions<MvcOptions> mvcOptions)
    {
        this.modelBinderFactory = modelBinderFactory;
        this.modelMetadataProvider = modelMetadataProvider;
        this.mvcOptions = mvcOptions;
    }

    public async Task<T> Bind<T>(HttpRequest request) where T : new()
    {
        //always rewind the stream; otherwise,
        //if multiple handlers concurrently bind
        //different models only the first one succeeds
        request.Body.Position = 0;

        var modelType = typeof(T);
        var modelMetadata = modelMetadataProvider.GetMetadataForType(modelType);
        var actionContext = new ActionContext(
            request.HttpContext,
            request.HttpContext.GetRouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());
        var valueProvider =
            await CompositeValueProvider.CreateAsync(actionContext, mvcOptions.Value.ValueProviderFactories);

#if NET5_0
        if (modelMetadata.BoundConstructor != null)
        {
            throw new NotSupportedException("Record type not supported");
        }
#endif

        var modelBindingContext = DefaultModelBindingContext.CreateBindingContext(
            actionContext,
            valueProvider,
            modelMetadata,
            bindingInfo: null,
            modelName: "");

        modelBindingContext.Model = new T();
        modelBindingContext.PropertyFilter = _ => true; // All props

        var factoryContext = new ModelBinderFactoryContext()
        {
            Metadata = modelMetadata,
            BindingInfo = new BindingInfo()
            {
                BinderModelName = modelMetadata.BinderModelName,
                BinderType = modelMetadata.BinderType,
                BindingSource = modelMetadata.BindingSource,
                PropertyFilterProvider = modelMetadata.PropertyFilterProvider,
            },
            CacheToken = modelMetadata,
        };

        await modelBinderFactory
            .CreateBinder(factoryContext)
            .BindModelAsync(modelBindingContext);

        return (T) modelBindingContext.Result.Model;
    }

    public async Task<object> Bind(HttpRequest request,Type type) 
    {
        //always rewind the stream; otherwise,
        //if multiple handlers concurrently bind
        //different models only the first one succeeds
        request.Body.Position = 0;

        var modelType = type;
        var modelMetadata = modelMetadataProvider.GetMetadataForType(modelType);
        var actionContext = new ActionContext(
            request.HttpContext,
            request.HttpContext.GetRouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());
        var valueProvider =
            await CompositeValueProvider.CreateAsync(actionContext, mvcOptions.Value.ValueProviderFactories);

#if NET5_0
        if (modelMetadata.BoundConstructor != null)
        {
            throw new NotSupportedException("Record type not supported");
        }
#endif

        var modelBindingContext = DefaultModelBindingContext.CreateBindingContext(
            actionContext,
            valueProvider,
            modelMetadata,
            bindingInfo: null,
            modelName: "");

        modelBindingContext.Model = Activator.CreateInstance(type);
        modelBindingContext.PropertyFilter = _ => true; // All props

        var factoryContext = new ModelBinderFactoryContext()
        {
            Metadata = modelMetadata,
            BindingInfo = new BindingInfo()
            {
                BinderModelName = modelMetadata.BinderModelName,
                BinderType = modelMetadata.BinderType,
                BindingSource = modelMetadata.BindingSource,
                PropertyFilterProvider = modelMetadata.PropertyFilterProvider,
            },
            CacheToken = modelMetadata,
        };

        await modelBinderFactory
            .CreateBinder(factoryContext)
            .BindModelAsync(modelBindingContext);

        return Convert.ChangeType(modelBindingContext.Result.Model,type);
    }
}