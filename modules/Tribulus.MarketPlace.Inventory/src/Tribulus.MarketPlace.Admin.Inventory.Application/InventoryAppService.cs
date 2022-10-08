using Tribulus.MarketPlace.Admin.Inventory.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Inventory;

public abstract class InventoryAppService : ApplicationService
{
    protected InventoryAppService()
    {
        LocalizationResource = typeof(InventoryResource);
        ObjectMapperContext = typeof(InventoryApplicationModule);
    }
}
