using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpServiceApplicationModule),
    typeof(AbpServiceDomainTestModule)
    )]
public class AbpServiceApplicationTestModule : AbpModule
{

}
