using Tribulus.MarketPlace.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(MarketPlaceEntityFrameworkCoreTestModule)
    )]
public class MarketPlaceDomainTestModule : AbpModule
{

}
