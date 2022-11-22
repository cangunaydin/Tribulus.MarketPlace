using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AggregateServiceDomainSharedModule)
)]
public class AggregateServiceDomainModule : AbpModule
{

}
