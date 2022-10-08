using Tribulus.MarketPlace.Inventory.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Inventory;

public abstract class InventoryAppService : ApplicationService
{
    protected InventoryAppService()
    {
        LocalizationResource = typeof(InventoryResource);
        ObjectMapperContext = typeof(InventoryApplicationModule);
    }
}
