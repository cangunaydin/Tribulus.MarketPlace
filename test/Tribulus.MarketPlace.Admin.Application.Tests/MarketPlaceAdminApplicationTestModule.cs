using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin;

[DependsOn(
    typeof(MarketPlaceAdminApplicationModule),
    typeof(MarketPlaceDomainTestModule)
    )]
public class MarketPlaceAdminApplicationTestModule : AbpModule
{

}
