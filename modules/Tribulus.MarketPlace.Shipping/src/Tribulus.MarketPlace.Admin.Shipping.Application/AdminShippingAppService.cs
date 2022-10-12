using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Shipping;

/* Inherit your application services from this class.
 */
public abstract class AdminShippingAppService : ApplicationService
{
    protected AdminShippingAppService()
    {
        LocalizationResource = typeof(ShippingResource);
    }
}
