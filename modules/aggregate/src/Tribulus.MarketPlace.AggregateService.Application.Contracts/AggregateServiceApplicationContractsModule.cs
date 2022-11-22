using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AggregateServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class AggregateServiceApplicationContractsModule : AbpModule
{

}
