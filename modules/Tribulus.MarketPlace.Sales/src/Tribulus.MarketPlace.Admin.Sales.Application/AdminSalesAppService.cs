using Tribulus.MarketPlace.Sales.Localization;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Sales;

public abstract class AdminSalesAppService : ApplicationService
{
    protected AdminSalesAppService()
    {
        LocalizationResource = typeof(SalesResource);
        ObjectMapperContext = typeof(AdminSalesApplicationModule);
    }
}
