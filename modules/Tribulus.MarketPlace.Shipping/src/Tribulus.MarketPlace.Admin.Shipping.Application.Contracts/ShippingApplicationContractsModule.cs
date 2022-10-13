using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tribulus.MarketPlace.Shipping;

namespace Tribulus.MarketPlace.Admin.Shipping;

[DependsOn(
    typeof(ShippingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ShippingApplicationContractsModule : AbpModule
{

}
