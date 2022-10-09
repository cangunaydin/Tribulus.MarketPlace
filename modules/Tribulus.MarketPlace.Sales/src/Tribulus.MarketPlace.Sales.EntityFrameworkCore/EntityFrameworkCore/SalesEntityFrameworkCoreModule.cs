using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Sales.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore;

[DependsOn(
    typeof(SalesDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class SalesEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SalesDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<ProductPrice, ProductPriceRepository>();
            options.AddRepository<Order, OrderRepository>();
        });
        Configure<AbpEntityOptions>(options =>
        {
            options.Entity<Order>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query
                => query
                .Include(f => f.OrderItems);

            });
        });
    }
}
