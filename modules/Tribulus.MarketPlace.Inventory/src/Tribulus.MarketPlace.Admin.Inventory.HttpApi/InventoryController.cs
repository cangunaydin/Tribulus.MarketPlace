using Tribulus.MarketPlace.Admin.Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Inventory;

public abstract class InventoryController : AbpControllerBase
{
    protected InventoryController()
    {
        LocalizationResource = typeof(InventoryResource);
    }
}
