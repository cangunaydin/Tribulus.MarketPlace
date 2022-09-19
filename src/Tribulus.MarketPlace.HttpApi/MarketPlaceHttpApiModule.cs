using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(MarketPlaceApplicationContractsModule)
    //typeof(AbpAccountHttpApiModule),
    //typeof(AbpIdentityHttpApiModule),
    //typeof(AbpPermissionManagementHttpApiModule),
    //typeof(AbpTenantManagementHttpApiModule),
    //typeof(AbpFeatureManagementHttpApiModule),
    //typeof(AbpSettingManagementHttpApiModule)
    )]
public class MarketPlaceHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MarketPlaceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
