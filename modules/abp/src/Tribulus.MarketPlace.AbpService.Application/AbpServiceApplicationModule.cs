using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.FeatureManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpServiceDomainModule),
    typeof(AbpServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
    [DependsOn(typeof(AbpPermissionManagementApplicationModule))]
    [DependsOn(typeof(AbpSettingManagementApplicationModule))]
    [DependsOn(typeof(AbpIdentityApplicationModule))]
    [DependsOn(typeof(AbpTenantManagementApplicationModule))]
    public class AbpServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpServiceApplicationModule>(validate: true);
        });
    }
}
