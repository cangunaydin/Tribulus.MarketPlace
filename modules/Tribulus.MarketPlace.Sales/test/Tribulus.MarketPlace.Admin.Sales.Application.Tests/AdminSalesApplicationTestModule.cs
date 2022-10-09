using Tribulus.MarketPlace.Sales;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Sales;

[DependsOn(
    typeof(AdminSalesApplicationModule),
    typeof(SalesDomainTestModule)
    )]
public class AdminSalesApplicationTestModule : AbpModule
{

}
