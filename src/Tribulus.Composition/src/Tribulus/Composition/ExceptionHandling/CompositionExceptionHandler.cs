using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;

namespace Tribulus.Composition.ExceptionHandling;

[ExposeServices(typeof(CompositionExceptionHandler))]
public class CompositionExceptionHandler : ITransientDependency
{
    public virtual async Task OnExceptionAsync(HttpContext context, Exception ex)
    {
        if (!ShouldHandleException(context))
        {
            return;
        }

        await HandleAndWrapException(context, ex);
    }

    protected virtual bool ShouldHandleException(HttpContext context)
    {
        //TODO: Create DontWrap attribute to control wrapping..?

        //if (context.Request.CanAccept(MimeTypes.Application.Json))
        //{
        //    return true;
        //}

        //if (context.Request.IsAjax())
        //{
        //    return true;
        //}
        //if it is preferred to handle exceptions in some conditions it can be modified here.
        return true;
    }

    protected virtual async Task HandleAndWrapException(HttpContext context, Exception exception)
    {
        //TODO: Trigger an AbpExceptionHandled event or something like that.

        var serviceProvider = context.RequestServices;
        var exceptionHandlingOptions = serviceProvider.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = serviceProvider.GetRequiredService<IExceptionToErrorInfoConverter>();
        var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(exception, options =>
        {
            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
        });

        var logLevel = exception.GetLogLevel();

        var remoteServiceErrorInfoBuilder = new StringBuilder();
        remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
        remoteServiceErrorInfoBuilder.AppendLine(serviceProvider.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

        var logger = serviceProvider.GetService<ILogger<AbpExceptionFilter>>();
        if (logger == null)
            logger = NullLogger<AbpExceptionFilter>.Instance;

        logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

        logger.LogException(exception, logLevel);

        await serviceProvider.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(exception));

        if (exception is AbpAuthorizationException)
        {
            await context.RequestServices.GetRequiredService<IAbpAuthorizationExceptionHandler>()
                .HandleAsync(exception.As<AbpAuthorizationException>(), context);
        }
        else
        {
            context.Response.Headers.Add(AbpHttpConsts.AbpErrorFormat, "true");
            context.Response.StatusCode = (int)serviceProvider
                .GetRequiredService<IHttpExceptionStatusCodeFinder>()
                .GetStatusCode(context, exception);

            await context.ExecuteResultAsync(new ObjectResult(new RemoteServiceErrorResponse(remoteServiceErrorInfo)));
        }

    }
}
