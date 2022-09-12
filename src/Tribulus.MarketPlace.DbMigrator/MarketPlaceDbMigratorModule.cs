using Tribulus.MarketPlace.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MarketPlaceEntityFrameworkCoreModule),
    typeof(MarketPlaceApplicationContractsModule)
    )]
public class MarketPlaceDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
