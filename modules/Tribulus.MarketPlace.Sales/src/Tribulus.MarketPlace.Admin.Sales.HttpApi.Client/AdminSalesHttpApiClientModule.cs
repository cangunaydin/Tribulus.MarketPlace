using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace.Admin.Sales;

[DependsOn(
    typeof(AdminSalesApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AdminSalesHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AdminSalesApplicationContractsModule).Assembly,
            AdminSalesRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdminSalesHttpApiClientModule>();
        });

    }
}
