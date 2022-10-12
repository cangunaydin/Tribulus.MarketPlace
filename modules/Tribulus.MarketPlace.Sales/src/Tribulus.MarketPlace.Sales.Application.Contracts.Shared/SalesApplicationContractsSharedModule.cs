using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales.Application.Contracts.Shared
{
    [DependsOn(
    typeof(SalesDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
    public class SalesApplicationContractsSharedModule : AbpModule
    {
    }
}
