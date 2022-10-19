using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Shipping;

public abstract class AdminShippingAppService : ApplicationService
{
    protected AdminShippingAppService()
    {
        LocalizationResource = typeof(ShippingResource);
        ObjectMapperContext = typeof(AdminShippingApplicationModule);
    }
}
