using Tribulus.MarketPlace.Admin.Marketing.Composition;
using Tribulus.MarketPlace.Composition;
using Tribulus.MarketPlace.Sales.Localization;

namespace Tribulus.MarketPlace.Admin.Marketing;

public abstract class AdminSalesCompositionService : CompositionService
{
    protected AdminSalesCompositionService()
    {
        LocalizationResource = typeof(SalesResource);
        ObjectMapperContext = typeof(AdminSalesCompositionModule);
    }
}
