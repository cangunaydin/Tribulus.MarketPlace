using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Sales.EntityFrameworkCore.Repositories;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore;

[DependsOn(
    typeof(SalesDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule)
)]
public class SalesEntityFrameworkCoreModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SalesEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SalesDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */

            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<ProductPrice, ProductPriceRepository>();
            options.AddRepository<Order, OrderRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<SalesDbContext>(c =>
            {
                c.UseSqlServer(b =>
                {
                    b.MigrationsHistoryTable("__SalesService_Migrations");
                });
            });
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
