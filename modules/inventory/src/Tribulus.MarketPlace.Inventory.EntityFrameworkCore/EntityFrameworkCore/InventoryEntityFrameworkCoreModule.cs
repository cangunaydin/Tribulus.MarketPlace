using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Inventory.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Inventory.Orders;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore;

[DependsOn(
    typeof(InventoryDomainModule),
     typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class InventoryEntityFrameworkCoreModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        InventoryEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InventoryDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddRepository<ProductStock, ProductStockRepository>();
            options.AddRepository<OrderItemQuantity, OrderItemQuantityRepository>();
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<InventoryDbContext>(c =>
            {
                c.UseSqlServer(b =>
                {
                    b.MigrationsHistoryTable("__InventoryService_Migrations");
                });
            });
        });
    }
}
