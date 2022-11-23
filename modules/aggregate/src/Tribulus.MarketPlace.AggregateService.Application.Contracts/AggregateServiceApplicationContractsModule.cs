using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Inventory;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AggregateServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AdminMarketingApplicationContractsModule),
    typeof(AdminSalesApplicationContractsModule),
    typeof(AdminInventoryApplicationContractsModule),
    typeof(SharedContractsModule)
    )]
public class AggregateServiceApplicationContractsModule : AbpModule
{

}
