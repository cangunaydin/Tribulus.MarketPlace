using Tribulus.MarketPlace.Inventory.Application.Contracts.Shared;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory;

[DependsOn(
    typeof(InventoryApplicationContractsSharedModule)
    )]
public class InventoryApplicationContractsModule : AbpModule
{

}
