using Tribulus.MarketPlace.Marketing.Application.Contracts.Shared;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(MarketingApplicationContractsSharedModule)
    )]
public class MarketingApplicationContractsModule : AbpModule
{

}
