using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Sales.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.Sales;

[DependsOn(
    typeof(AdminSalesApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AdminSalesHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AdminSalesHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SalesResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
