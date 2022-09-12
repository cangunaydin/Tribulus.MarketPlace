using Tribulus.MarketPlace.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MarketPlaceController : AbpControllerBase
{
    protected MarketPlaceController()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }
}
