using Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AggregateService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(AggregateServiceEntityFrameworkCoreTestModule)
    )]
public class AggregateServiceDomainTestModule : AbpModule
{

}
