using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
    typeof(AdminMarketingApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AdminMarketingHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AdminMarketingHttpApiModule).Assembly);
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
