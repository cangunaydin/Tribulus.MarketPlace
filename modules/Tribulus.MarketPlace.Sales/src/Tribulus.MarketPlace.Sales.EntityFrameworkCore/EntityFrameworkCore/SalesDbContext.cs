using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore;

[ConnectionStringName(SalesDbProperties.ConnectionStringName)]
public class SalesDbContext : AbpDbContext<SalesDbContext>, ISalesDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductPrice> ProductPrices { get; set; }
    public SalesDbContext(DbContextOptions<SalesDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSales();
    }
}
