using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(ShippingApplicationModule),
    typeof(ShippingDomainTestModule)
    )]
public class ShippingApplicationTestModule : AbpModule
{

}
