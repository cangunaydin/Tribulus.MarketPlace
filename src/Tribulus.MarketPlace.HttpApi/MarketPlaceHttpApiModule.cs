using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(MarketPlaceApplicationContractsModule)
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
