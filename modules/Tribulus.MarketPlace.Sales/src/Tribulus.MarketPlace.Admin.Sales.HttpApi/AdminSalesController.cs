using Tribulus.MarketPlace.Sales.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Sales;

public abstract class AdminSalesController : AbpControllerBase
{
    protected AdminSalesController()
    {
        LocalizationResource = typeof(SalesResource);
    }
}
