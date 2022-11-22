using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace.Admin.Inventory;

[DependsOn(
    typeof(AdminInventoryApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AdminInventoryHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AdminInventoryApplicationContractsModule).Assembly,
            AdminInventoryRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdminInventoryHttpApiClientModule>();
        });

    }
}
