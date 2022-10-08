using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Sales;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Inventory;

namespace Tribulus.MarketPlace.Admin;

[DependsOn(
    typeof(MarketPlaceDomainSharedModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpObjectExtendingModule)
)]
[DependsOn(typeof(MarketingApplicationContractsModule))]
    [DependsOn(typeof(SalesApplicationContractsModule))]
    [DependsOn(typeof(InventoryApplicationContractsModule))]
    public class AdminApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdminDtoExtensions.Configure();
    }
}
