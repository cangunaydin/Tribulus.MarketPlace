using Tribulus.MarketPlace.Sales.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Sales;

public abstract class SalesController : AbpControllerBase
{
    protected SalesController()
    {
        LocalizationResource = typeof(SalesResource);
    }
}
