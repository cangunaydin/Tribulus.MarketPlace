using Tribulus.MarketPlace.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin;

/* Inherit your application services from this class.
 */
public abstract class MarketPlaceAdminAppService : ApplicationService
{
    protected MarketPlaceAdminAppService()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
