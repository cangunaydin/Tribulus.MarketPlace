using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.FeatureManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
    [DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
    [DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
    [DependsOn(typeof(AbpIdentityHttpApiClientModule))]
    [DependsOn(typeof(AbpTenantManagementHttpApiClientModule))]
    public class AbpServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AbpServiceApplicationContractsModule).Assembly,
            AbpServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpServiceHttpApiClientModule>();
        });

    }
}
