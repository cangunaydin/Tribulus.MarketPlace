using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales.Blazor.WebAssembly;

[DependsOn(
    typeof(SalesBlazorModule),
    typeof(SalesHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class SalesBlazorWebAssemblyModule : AbpModule
{

}
