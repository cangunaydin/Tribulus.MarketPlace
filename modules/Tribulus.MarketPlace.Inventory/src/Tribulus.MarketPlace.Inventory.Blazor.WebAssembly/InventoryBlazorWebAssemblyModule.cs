using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory.Blazor.WebAssembly;

[DependsOn(
    typeof(InventoryBlazorModule),
    typeof(InventoryHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class InventoryBlazorWebAssemblyModule : AbpModule
{

}
