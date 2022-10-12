using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shipping.Application.Contracts.Shared
{

    [DependsOn(
      typeof(ShippingDomainSharedModule),
      typeof(AbpDddApplicationContractsModule),
      typeof(AbpAuthorizationModule)
      )]
    public class ShippingApplicationContractsSharedModule : AbpModule
    {
    }
}
