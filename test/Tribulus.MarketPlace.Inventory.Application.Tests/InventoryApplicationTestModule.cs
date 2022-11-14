using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory;

[DependsOn(
    typeof(InventoryApplicationModule),
    typeof(InventoryDomainTestModule)
    )]
public class InventoryApplicationTestModule : AbpModule
{

}
