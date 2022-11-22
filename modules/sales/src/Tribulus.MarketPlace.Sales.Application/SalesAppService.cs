using Tribulus.MarketPlace.Sales.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Sales;

public abstract class SalesAppService : ApplicationService
{
    protected SalesAppService()
    {
        LocalizationResource = typeof(SalesResource);
        ObjectMapperContext = typeof(SalesApplicationModule);
    }
}
