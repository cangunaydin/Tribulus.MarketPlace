using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace.Admin.Shipping;

[DependsOn(
    typeof(AdminShippingApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ShippingHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AdminShippingApplicationContractsModule).Assembly,
            AdminShippingRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ShippingHttpApiClientModule>();
        });

    }
}
