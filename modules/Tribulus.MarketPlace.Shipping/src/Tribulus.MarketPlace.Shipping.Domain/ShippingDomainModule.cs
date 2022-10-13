using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ShippingDomainSharedModule)
)]
public class ShippingDomainModule : AbpModule
{

}
