using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Sales;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Inventory;

namespace Tribulus.MarketPlace.Admin;

[DependsOn(
    typeof(AdminApplicationContractsModule),
    typeof(AbpAccountHttpApiClientModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
[DependsOn(typeof(MarketingHttpApiClientModule))]
    [DependsOn(typeof(SalesHttpApiClientModule))]
    [DependsOn(typeof(InventoryHttpApiClientModule))]
    public class AdminHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AdminApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdminHttpApiClientModule>();
        });
    }
}
