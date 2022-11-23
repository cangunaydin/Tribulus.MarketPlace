
using Tribulus.MarketPlace.AbpService;
using Tribulus.MarketPlace.AbpService.EntityFrameworkCore;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.AggregateService;
using Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;
using Tribulus.MarketPlace.Inventory.EntityFrameworkCore;
using Tribulus.MarketPlace.Marketing.EntityFrameworkCore;
using Tribulus.MarketPlace.Sales.EntityFrameworkCore;
using Tribulus.MarketPlace.Shared.Hosting;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.DbMigrator;

[DependsOn(
    typeof(MarketPlaceSharedHostingModule),
    typeof(AbpServiceEntityFrameworkCoreModule),
    typeof(AbpServiceApplicationContractsModule),

    typeof(AggregateServiceEntityFrameworkCoreModule),
    typeof(AggregateServiceApplicationContractsModule),

    typeof(MarketingEntityFrameworkCoreModule),
    typeof(AdminMarketingApplicationContractsModule),
    
    typeof(SalesEntityFrameworkCoreModule),
    typeof(AdminSalesApplicationContractsModule),
    
    typeof(InventoryEntityFrameworkCoreModule),
    typeof(AdminInventoryApplicationContractsModule)
)]
public class MarketPlaceDbMigratorModule : AbpModule
{

}
