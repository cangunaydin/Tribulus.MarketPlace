using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(InventoryDomainSharedModule)
)]
public class InventoryDomainModule : AbpModule
{

}
