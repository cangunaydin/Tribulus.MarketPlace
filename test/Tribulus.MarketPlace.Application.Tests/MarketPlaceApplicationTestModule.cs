using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(MarketPlaceApplicationModule),
    typeof(MarketPlaceDomainTestModule)
    )]
public class MarketPlaceApplicationTestModule : AbpModule
{

}
