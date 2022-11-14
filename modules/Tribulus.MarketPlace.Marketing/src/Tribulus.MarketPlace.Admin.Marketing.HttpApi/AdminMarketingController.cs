using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Marketing;

public abstract class AdminMarketingController : AbpControllerBase
{
    protected AdminMarketingController()
    {
        LocalizationResource = typeof(MarketingResource);
    }
}
