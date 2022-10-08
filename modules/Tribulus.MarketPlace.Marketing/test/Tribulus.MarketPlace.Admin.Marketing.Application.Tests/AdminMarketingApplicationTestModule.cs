
using Tribulus.MarketPlace.Marketing;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
    typeof(AdminMarketingApplicationModule),
    typeof(MarketingDomainTestModule)
    )]
public class AdminMarketingApplicationTestModule : AbpModule
{

}
