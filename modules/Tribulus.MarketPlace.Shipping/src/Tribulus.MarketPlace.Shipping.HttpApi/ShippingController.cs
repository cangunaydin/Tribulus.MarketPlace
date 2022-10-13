using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Shipping;

public abstract class ShippingController : AbpControllerBase
{
    protected ShippingController()
    {
        LocalizationResource = typeof(ShippingResource);
    }
}
