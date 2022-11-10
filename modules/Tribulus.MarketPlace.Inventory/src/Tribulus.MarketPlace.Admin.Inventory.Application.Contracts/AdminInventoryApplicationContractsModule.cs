using Tribulus.MarketPlace.Inventory.Application.Contracts.Shared;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Inventory;

[DependsOn(
    typeof(InventoryApplicationContractsSharedModule)
    )]
public class AdminInventoryApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdminInventoryDtoExtensions.Configure();
    }
}
