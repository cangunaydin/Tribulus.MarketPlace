using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MarketingDomainSharedModule)
)]
public class MarketingDomainModule : AbpModule
{

}
