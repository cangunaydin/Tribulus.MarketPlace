using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
[DependsOn(typeof(AbpFeatureManagementApplicationContractsModule))]
    [DependsOn(typeof(AbpPermissionManagementApplicationContractsModule))]
    [DependsOn(typeof(AbpSettingManagementApplicationContractsModule))]
    [DependsOn(typeof(AbpIdentityApplicationContractsModule))]
    [DependsOn(typeof(AbpTenantManagementApplicationContractsModule))]
    public class AbpServiceApplicationContractsModule : AbpModule
{

}
