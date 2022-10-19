using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(ShippingApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class ShippingHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ShippingHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ShippingResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
