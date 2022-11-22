using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore;

public static class SalesDbContextModelCreatingExtensions
{
    public static void ConfigureSales(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ProductPrice>(b =>
        {
            b.ToTable(SalesDbProperties.DbTablePrefix + "ProductPrices", SalesDbProperties.DbSchema);

            b.ConfigureByConvention();

        });

        builder.Entity<Order>(b =>
        {
            b.ToTable(SalesDbProperties.DbTablePrefix + "Orders", SalesDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(OrderConsts.MaxNameLength);


            b.HasIndex(x => x.Name);

            b.HasMany(x => x.OrderItems).WithOne().IsRequired().HasForeignKey(x => x.OrderId);
        });

        builder.Entity<OrderItem>(b =>
        {
            b.ToTable(SalesDbProperties.DbTablePrefix + "OrderItems", SalesDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<ProductPrice>().WithMany().HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });

    }
}
