using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tribulus.MarketPlace.Shipping;
using Tribulus.MarketPlace.Marketing.Application.Contracts.Shared;

namespace Tribulus.MarketPlace.Admin.Shipping;

[DependsOn(
    typeof(ShippingApplicationContractsSharedModule)
    )]
public class AdminShippingApplicationContractsModule : AbpModule
{

}
