using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Shipping.EntityFrameworkCore;

[ConnectionStringName(ShippingDbProperties.ConnectionStringName)]
public class ShippingDbContext : AbpDbContext<ShippingDbContext>, IShippingDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<ProductShippingOptions> ProductShippingOptions { get; set; }


    public ShippingDbContext(DbContextOptions<ShippingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureShipping();
    }
}
