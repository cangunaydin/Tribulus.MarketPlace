using Tribulus.MarketPlace.Shared.Hosting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shared.Hosting.Gateways;

[DependsOn(
    typeof(MarketPlaceSharedHostingAspNetCoreModule)
)]
public class MarketPlaceSharedHostingGatewaysModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));
    }
}