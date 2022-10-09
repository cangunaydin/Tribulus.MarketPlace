using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tribulus.MarketPlace.Inventory.Application.Contracts.Shared;

namespace Tribulus.MarketPlace.Admin.Inventory;

[DependsOn(
    typeof(InventoryApplicationContractsSharedModule)
    )]
public class InventoryApplicationContractsModule : AbpModule
{

}
