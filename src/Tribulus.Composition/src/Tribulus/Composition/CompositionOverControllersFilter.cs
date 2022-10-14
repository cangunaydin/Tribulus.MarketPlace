using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Tribulus.Composition.ExceptionHandling;
using Volo.Abp.DependencyInjection;

namespace Tribulus.Composition.Tribulus.Composition
{
    public class CompositionOverControllersFilter : IAsyncActionFilter, ITransientDependency
    {
        CompositionOptions _compositionOptions;
        CompositionExceptionHandler _exceptionHandler;
        public CompositionOverControllersFilter(CompositionOptions compositionOptions,
            CompositionExceptionHandler exceptionHandler)
        {
            _compositionOptions = compositionOptions;
            _exceptionHandler = exceptionHandler;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.GetEndpoint() is RouteEndpoint endpoint)
            {
                Debug.Assert(endpoint.RoutePattern.RawText != null, "endpoint.RoutePattern.RawText != null");
                var rawTemplate = endpoint.RoutePattern.RawText;
                var templateHttpMethod = context.HttpContext.Request.Method;
                var compositionRouteMatched = _compositionOptions.CompositionRouteRegistry.Where(o => o.Route == rawTemplate && o.Method.Method == templateHttpMethod).FirstOrDefault();
                //var handlerTypes = _compositionOverControllersRoutes.HandlersForRoute(rawTemplate, );

                if (compositionRouteMatched != null)
                {
                    var compositionHandler = new CompositionHandler();
                    var viewModel = await compositionHandler.HandleComposableRequest(context.HttpContext, compositionRouteMatched);
                    if (viewModel != null)
                        context.Result = new ObjectResult(viewModel);
                    else
                        context.Result = new OkResult();
                    return;

                }
            }

            await next();
        }

    }
}
