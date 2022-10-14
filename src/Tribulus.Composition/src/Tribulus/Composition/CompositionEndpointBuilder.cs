using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Tribulus.Composition.ExceptionHandling;

namespace Tribulus.Composition;

class CompositionEndpointBuilder : EndpointBuilder
{

    readonly RoutePattern routePattern;

    public int Order { get; }


    public CompositionEndpointBuilder(RoutePattern routePattern, CompositionRouteRegistry compositionRouteRegistry, int order, bool useOutputFormatters = true)
    {
        this.routePattern = routePattern;
        Order = order;
        RequestDelegate = async httpContext =>
        {
            var exceptionHandler = httpContext.RequestServices.GetRequiredService<CompositionExceptionHandler>();

            try
            {
                var viewModelType = compositionRouteRegistry.ViewModelType;
                var compositionHandler = new CompositionHandler();
                var viewModel = await compositionHandler.HandleComposableRequest(httpContext, compositionRouteRegistry);
                if (viewModel != null)
                {

                    var containsActionResult = httpContext.Items.ContainsKey(HttpRequestExtensions.ComposedActionResultKey);
                    switch (useOutputFormatters)
                    {
                        case false when containsActionResult:
                            throw new NotSupportedException($"Setting an action results requires output formatters supports.");
                        case true when containsActionResult:
                            await httpContext.ExecuteResultAsync(httpContext.Items[HttpRequestExtensions.ComposedActionResultKey] as IActionResult);
                            break;
                        case true:
                            await httpContext.WriteModelAsync(viewModel);
                            break;
                        default:
                            {
                                var json = JsonConvert.SerializeObject(viewModel, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                httpContext.Response.ContentType = "application/json; charset=utf-8";
                                await httpContext.Response.WriteAsync(json);
                                break;
                            }
                    }
                }
                else
                {
                    await httpContext.Response.WriteAsync(string.Empty);
                }
            }
            catch (Exception ex)
            {
                await exceptionHandler.OnExceptionAsync(httpContext, ex);
            }
        };
    }



    public override Endpoint Build()
    {
        var routeEndpoint = new RouteEndpoint(
            RequestDelegate,
            routePattern,
            Order,
            new EndpointMetadataCollection(Metadata),
            DisplayName);

        return routeEndpoint;
    }
}