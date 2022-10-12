using Tribulus.MarketPlace.Shipping.Application.Contracts.Shared;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Shipping;

[DependsOn(
    typeof(ShippingApplicationContractsSharedModule)
)]
public class AdminShippingApplicationContractsModule : AbpModule
{
}
