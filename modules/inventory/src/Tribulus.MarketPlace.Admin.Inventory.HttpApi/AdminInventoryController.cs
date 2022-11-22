using Tribulus.MarketPlace.Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Inventory;

public abstract class AdminInventoryController : AbpControllerBase
{
    protected AdminInventoryController()
    {
        LocalizationResource = typeof(InventoryResource);
    }
}
