using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Tribulus.MarketPlace.AbpService.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpValidationModule)
)]
[DependsOn(typeof(AbpAuditLoggingDomainSharedModule))]
    [DependsOn(typeof(AbpFeatureManagementDomainSharedModule))]
    [DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
    [DependsOn(typeof(AbpSettingManagementDomainSharedModule))]
    [DependsOn(typeof(AbpIdentityDomainSharedModule))]
    [DependsOn(typeof(AbpOpenIddictDomainSharedModule))]
    [DependsOn(typeof(AbpTenantManagementDomainSharedModule))]
    public class AbpServiceDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpServiceDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpServiceResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/AbpService");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("AbpService", typeof(AbpServiceResource));
        });
    }
}
