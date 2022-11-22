using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AggregateServiceApplicationModule),
    typeof(AggregateServiceDomainTestModule)
    )]
public class AggregateServiceApplicationTestModule : AbpModule
{

}
