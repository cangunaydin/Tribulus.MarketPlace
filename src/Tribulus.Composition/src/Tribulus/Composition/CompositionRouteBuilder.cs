using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http;
using Volo.Abp.Reflection;

namespace Tribulus.Composition;

public class CompositionRouteBuilder : ICompositionRouteBuilder, ITransientDependency
{
    public string Build(string rootPath, 
        string className, 
        MethodInfo methodInfo, 
        string httpMethod, 
        [CanBeNull] ConventionalControllerSetting configuration)
    {
        var apiRoutePrefix = GetApiRoutePrefix(methodInfo, configuration);
        className=className.RemovePostFix("Controller")
            .RemovePostFix(CompositionService.CommonPostfixes);
        var controllerNameInUrl =
            NormalizeUrlControllerName(rootPath, className, methodInfo, httpMethod, configuration);

        var url = $"{apiRoutePrefix}/{rootPath}/{NormalizeControllerNameCase(controllerNameInUrl, configuration)}";

        //Add {id} path if needed
        var idParameterModel = methodInfo.GetParameters().FirstOrDefault(p => p.Name == "id");
        if (idParameterModel != null)
        {
            if (TypeHelper.IsPrimitiveExtended(idParameterModel.ParameterType, includeEnums: true))
            {
                url += "/{id}";
            }
            else
            {
                var properties = idParameterModel
                    .ParameterType
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var property in properties)
                {
                    url += "/{" + NormalizeIdPropertyNameCase(property, configuration) + "}";
                }
            }
        }

        //Add action name if needed
        var actionNameInUrl = NormalizeUrlActionName(rootPath, className, methodInfo, httpMethod, configuration);
        if (!actionNameInUrl.IsNullOrEmpty())
        {
            url += $"/{NormalizeActionNameCase(actionNameInUrl, configuration)}";

            //Add secondary Id
            var secondaryIds = methodInfo.GetParameters()
                .Where(p => p.Name.EndsWith("Id", StringComparison.Ordinal)).ToList();
            if (secondaryIds.Count == 1)
            {
                url += $"/{{{NormalizeSecondaryIdNameCase(secondaryIds[0], configuration)}}}";
            }
        }

        return url;
    }


    protected virtual string GetApiRoutePrefix(MethodInfo methodInfo, ConventionalControllerSetting configuration)
    {

        return CompositionConsts.CompositionApiPrefix;
    }

    protected virtual string NormalizeUrlActionName(string rootPath, string controllerName, MethodInfo methodInfo,
        string httpMethod, [CanBeNull] ConventionalControllerSetting configuration)
    {
        var actionNameInUrl = HttpMethodHelper
            .RemoveHttpMethodPrefix(methodInfo.Name, httpMethod)
            .RemovePostFix("Async");

        return actionNameInUrl;
    }

    protected virtual string NormalizeUrlControllerName(string rootPath, string className, MethodInfo methodInfo,
        string httpMethod, [CanBeNull] ConventionalControllerSetting configuration)
    {
        if (configuration?.UrlControllerNameNormalizer == null)
        {
            return className;
        }

        return configuration.UrlControllerNameNormalizer(
            new UrlControllerNameNormalizerContext(
                rootPath,
                className
            )
        );
    }

    protected virtual string NormalizeControllerNameCase(string className,
        [CanBeNull] ConventionalControllerSetting configuration)
    {
            return className.ToKebabCase();

    }

    protected virtual string NormalizeActionNameCase(string methodName,
        [CanBeNull] ConventionalControllerSetting configuration)
    {

            return methodName.ToKebabCase();

    }

    protected virtual string NormalizeIdPropertyNameCase(PropertyInfo property,
        [CanBeNull] ConventionalControllerSetting configuration)
    {
        return property.Name;
    }

    protected virtual string NormalizeSecondaryIdNameCase(ParameterInfo secondaryId,
        [CanBeNull] ConventionalControllerSetting configuration)
    {
        return secondaryId.Name;
    }

   
}
