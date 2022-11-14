using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Marketing;

public abstract class MarketingAppService : ApplicationService
{
    protected MarketingAppService()
    {
        LocalizationResource = typeof(MarketingResource);
        ObjectMapperContext = typeof(MarketingApplicationModule);
    }
}
