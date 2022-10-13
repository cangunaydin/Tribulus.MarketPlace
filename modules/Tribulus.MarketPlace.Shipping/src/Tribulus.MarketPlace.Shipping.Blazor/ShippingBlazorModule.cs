using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Shipping.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Tribulus.MarketPlace.Shipping.Blazor;

[DependsOn(
    typeof(ShippingApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class ShippingBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ShippingBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<ShippingBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ShippingMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(ShippingBlazorModule).Assembly);
        });
    }
}
