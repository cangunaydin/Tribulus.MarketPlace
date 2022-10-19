using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Shipping;

public abstract class ShippingAppService : ApplicationService
{
    protected ShippingAppService()
    {
        LocalizationResource = typeof(ShippingResource);
        ObjectMapperContext = typeof(ShippingApplicationModule);
    }
}
