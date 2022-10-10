using Tribulus.MarketPlace.Admin.Marketing.Composition;
using Tribulus.MarketPlace.Composition;
using Tribulus.MarketPlace.Marketing.Localization;

namespace Tribulus.MarketPlace.Admin.Marketing;

public abstract class AdminMarketingCompositionService : CompositionService
{
    protected AdminMarketingCompositionService()
    {
        LocalizationResource = typeof(MarketingResource);
        ObjectMapperContext = typeof(AdminMarketingCompositionModule);
    }
}
