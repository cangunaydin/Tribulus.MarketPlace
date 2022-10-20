using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Inventory;

public abstract class AdminShippingController : AbpControllerBase
{
    protected AdminShippingController()
    {
        LocalizationResource = typeof(ShippingResource);
    }
}
