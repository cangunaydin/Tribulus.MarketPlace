using Tribulus.MarketPlace.Sales.Application.Contracts.Shared;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales;

[DependsOn(
    typeof(SalesApplicationContractsSharedModule)
    )]
public class SalesApplicationContractsModule : AbpModule
{

}
