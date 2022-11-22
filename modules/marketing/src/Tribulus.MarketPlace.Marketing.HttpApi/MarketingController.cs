using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Marketing;

public abstract class MarketingController : AbpControllerBase
{
    protected MarketingController()
    {
        LocalizationResource = typeof(MarketingResource);
    }
}
