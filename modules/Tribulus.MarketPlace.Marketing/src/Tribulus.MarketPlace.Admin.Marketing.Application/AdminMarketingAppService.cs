using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Marketing;

public abstract class AdminMarketingAppService : ApplicationService
{
    protected AdminMarketingAppService()
    {
        LocalizationResource = typeof(MarketingResource);
        ObjectMapperContext = typeof(AdminMarketingApplicationModule);
    }
}
