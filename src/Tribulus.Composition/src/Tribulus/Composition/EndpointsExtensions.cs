using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tribulus.Composition.Tribulus.Composition;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.Composition;

public static class EndpointsExtensions
{

    public static IEndpointConventionBuilder MapCompositionHandlers(this IEndpointRouteBuilder endpoints, ApplicationInitializationContext context)
    {
        if (endpoints == null)
        {
            throw new ArgumentNullException(nameof(endpoints));
        }

        var compositionOptions = context.ServiceProvider.GetRequiredService<CompositionOptions>();
        if (!compositionOptions.IsRegistered)
        {
            var compositionHandlers = context.ServiceProvider.GetServices<ICompositionHandleService>();
            var compositionSubscribers = context.ServiceProvider.GetServices<ICompositionSubscribeService>();
            compositionOptions.CreateCompositionRoutes(compositionHandlers, compositionSubscribers);
        }
      
        MapRoutes(compositionOptions.CompositionRouteRegistry, endpoints.DataSources);
        var dataSource = endpoints.DataSources.OfType<CompositionEndpointDataSource>().FirstOrDefault();
        return dataSource;
    }
    private static void MapRoutes(List<CompositionRouteRegistry> compositionRouteRegistries, ICollection<EndpointDataSource> dataSources)
    {
        foreach (var compositionRouteRegistry in compositionRouteRegistries)
        {
            var builder = CreateCompositionEndpointBuilder(compositionRouteRegistry);
            AppendToDataSource(dataSources, builder);
        }

    }

    private static void AppendToDataSource(ICollection<EndpointDataSource> dataSources,
        CompositionEndpointBuilder builder)
    {
        var dataSource = dataSources.OfType<CompositionEndpointDataSource>().FirstOrDefault();
        if (dataSource == null)
        {
            dataSource = new CompositionEndpointDataSource();
            dataSources.Add(dataSource);
        }

        dataSource.AddEndpointBuilder(builder);
    }



    private static CompositionEndpointBuilder CreateCompositionEndpointBuilder(
        CompositionRouteRegistry compositionRegistry)
    {
        var builder = new CompositionEndpointBuilder(
            RoutePatternFactory.Parse(compositionRegistry.Route),
            compositionRegistry,
            0)
        {
            DisplayName = compositionRegistry.Route,
        };

        var attributes = compositionRegistry.Handlers.SelectMany(o => o.MethodInfo.GetCustomAttributes()).Distinct();
        foreach (var attribute in attributes)
        {
            builder.Metadata.Add(attribute);
        }

        return builder;
    }


}