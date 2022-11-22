﻿using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.AggregateService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AggregateServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AggregateServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AggregateServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AggregateServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
