using Volo.Abp.AuditLogging;
using Volo.Abp.Domain;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpServiceDomainSharedModule),

    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpSettingManagementDomainModule),

    typeof(AbpPermissionManagementDomainModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),

    typeof(AbpTenantManagementDomainModule),
    typeof(AbpFeatureManagementDomainModule),

    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule)
    
)]
    public class AbpServiceDomainModule : AbpModule
{

}
