using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
    typeof(AdminMarketingApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AdminMarketingHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AdminMarketingApplicationContractsModule).Assembly,
            AdminMarketingRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdminMarketingHttpApiClientModule>();
        });

    }
}
