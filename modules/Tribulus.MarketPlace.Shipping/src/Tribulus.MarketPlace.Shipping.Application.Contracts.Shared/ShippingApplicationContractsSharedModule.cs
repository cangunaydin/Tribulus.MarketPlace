using System;
using Tribulus.MarketPlace.Shipping;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing.Application.Contracts.Shared
{
    [DependsOn(
    typeof(ShippingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]

    public class ShippingApplicationContractsSharedModule:AbpModule
    {

    }
}
