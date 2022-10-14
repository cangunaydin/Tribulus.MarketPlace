using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Tribulus.Composition;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
public class TribulusCompositionModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAssemblyOf<TribulusCompositionModule>();
    }
}