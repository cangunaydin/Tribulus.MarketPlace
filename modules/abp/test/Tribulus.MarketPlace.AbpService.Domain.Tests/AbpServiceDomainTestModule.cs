using Tribulus.MarketPlace.AbpService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AbpService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(AbpServiceEntityFrameworkCoreTestModule)
    )]
public class AbpServiceDomainTestModule : AbpModule
{

}
