using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Inventory.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Tribulus.MarketPlace.Inventory.Blazor;

[DependsOn(
    typeof(InventoryApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class InventoryBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<InventoryBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<InventoryBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new InventoryMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(InventoryBlazorModule).Assembly);
        });
    }
}
