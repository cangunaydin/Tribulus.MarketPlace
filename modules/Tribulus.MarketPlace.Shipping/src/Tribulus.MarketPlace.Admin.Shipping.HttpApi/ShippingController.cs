using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Shipping;

public abstract class ShippingController : AbpControllerBase
{
    protected ShippingController()
    {
        LocalizationResource = typeof(ShippingResource);
    }
}
