﻿using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Shipping.Localization;

namespace Tribulus.MarketPlace.Admin.Shipping;

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
