using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.Composition.ModelBinding;

public static class HttpRequestModelBinderExtension
{
    public static Task<T> Bind<T>(this HttpRequest request) where T : new()
    {
        var context = request.HttpContext;
        var binder = context.RequestServices.GetRequiredService<RequestModelBinder>();

        return binder.Bind<T>(request);
    }
    public static Task<object> Bind(this HttpRequest request,Type type)
    {
        var context = request.HttpContext;
        var binder = context.RequestServices.GetRequiredService<RequestModelBinder>();

        return binder.Bind(request,type);
    }
}