
using Tribulus.MarketPlace.AbpService;
using Tribulus.MarketPlace.AbpService.EntityFrameworkCore;
using Tribulus.MarketPlace.AggregateService;
using Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.DbMigrator;

[DependsOn(
    typeof(MarketPlaceSharedHostingModule),
    typeof(AbpServiceEntityFrameworkCoreModule),
    typeof(AbpServiceApplicationContractsModule),

    typeof(AggregateServiceEntityFrameworkCoreModule),
    typeof(AggregateServiceApplicationContractsModule)
)]
public class MarketPlaceDbMigratorModule : AbpModule
{

}
