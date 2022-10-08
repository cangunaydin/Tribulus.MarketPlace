using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(MarketingApplicationModule),
    typeof(MarketingDomainTestModule)
    )]
public class MarketingApplicationTestModule : AbpModule
{

}
