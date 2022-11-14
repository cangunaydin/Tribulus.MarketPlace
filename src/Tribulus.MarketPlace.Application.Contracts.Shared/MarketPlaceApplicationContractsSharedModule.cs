using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace Tribulus.MarketPlace.Application.Contracts.Shared
{
    [DependsOn(
    typeof(MarketPlaceDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]

    public class MarketPlaceApplicationContractsSharedModule:AbpModule
    {

    }
}