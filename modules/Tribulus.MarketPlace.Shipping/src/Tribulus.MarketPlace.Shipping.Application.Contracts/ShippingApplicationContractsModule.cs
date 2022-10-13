using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(ShippingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ShippingApplicationContractsModule : AbpModule
{

}
