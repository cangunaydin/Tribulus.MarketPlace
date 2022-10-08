using Localization.Resources.AbpUi;
using Tribulus.MarketPlace.Admin.Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.Inventory;

[DependsOn(
    typeof(InventoryApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class InventoryHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(InventoryHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<InventoryResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
