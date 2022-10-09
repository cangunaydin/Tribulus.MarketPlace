using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tribulus.MarketPlace.Sales.Application.Contracts.Shared;

namespace Tribulus.MarketPlace.Admin.Sales;

[DependsOn(
    typeof(SalesApplicationContractsSharedModule)
    )]
public class AdminSalesApplicationContractsModule : AbpModule
{

}
