using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales;

[DependsOn(
    typeof(SalesApplicationModule),
    typeof(SalesDomainTestModule)
    )]
public class SalesApplicationTestModule : AbpModule
{

}
