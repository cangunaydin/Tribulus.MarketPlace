using Tribulus.MarketPlace.Inventory.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Inventory;

public abstract class AdminInventoryAppService : ApplicationService
{
    protected AdminInventoryAppService()
    {
        LocalizationResource = typeof(InventoryResource);
        ObjectMapperContext = typeof(AdminInventoryApplicationModule);
    }
}
