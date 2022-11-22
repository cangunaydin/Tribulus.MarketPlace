using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(MarketingApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class MarketingHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MarketingHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MarketingResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
