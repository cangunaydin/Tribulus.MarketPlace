using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shipping.Blazor.WebAssembly;

[DependsOn(
    typeof(ShippingBlazorModule),
    typeof(ShippingHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class ShippingBlazorWebAssemblyModule : AbpModule
{

}
