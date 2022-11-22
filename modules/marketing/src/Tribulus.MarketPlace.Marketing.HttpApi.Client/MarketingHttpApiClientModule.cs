using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(MarketingApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class MarketingHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(MarketingApplicationContractsModule).Assembly,
            MarketingRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MarketingHttpApiClientModule>();
        });

    }
}
