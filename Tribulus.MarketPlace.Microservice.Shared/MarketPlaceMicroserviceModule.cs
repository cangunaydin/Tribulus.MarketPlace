using Tribulus.MarketPlace.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.Microservice.Shared;

[DependsOn(
    //typeof(TaskyHostingModule),
    typeof(MarketPlaceEntityFrameworkCoreModule)
)]
public class MarketPlaceMicroserviceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}