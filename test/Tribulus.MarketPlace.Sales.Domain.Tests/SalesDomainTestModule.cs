using Tribulus.MarketPlace.Sales.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(SalesEntityFrameworkCoreTestModule)
    )]
public class SalesDomainTestModule : AbpModule
{

}
