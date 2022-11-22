using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Inventory.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Inventory.Orders;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore;

[DependsOn(
    typeof(InventoryDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class InventoryEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InventoryDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<ProductStock, ProductStockRepository>();
            options.AddRepository<OrderItemQuantity, OrderItemQuantityRepository>();
        });
    }
}
