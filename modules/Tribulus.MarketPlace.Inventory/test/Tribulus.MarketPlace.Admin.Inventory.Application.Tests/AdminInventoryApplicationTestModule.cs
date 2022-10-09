using Tribulus.MarketPlace.Admin.Inventory;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory;

[DependsOn(
    typeof(AdminInventoryApplicationModule),
    typeof(InventoryDomainTestModule)
    )]
public class AdminInventoryApplicationTestModule : AbpModule
{

}
