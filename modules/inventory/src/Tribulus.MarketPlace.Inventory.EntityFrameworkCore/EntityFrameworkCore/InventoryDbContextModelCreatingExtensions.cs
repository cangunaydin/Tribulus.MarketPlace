using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Inventory.Orders;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore;

public static class InventoryDbContextModelCreatingExtensions
{
    public static void ConfigureInventory(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ProductStock>(b =>
        {
            b.ToTable(InventoryDbProperties.DbTablePrefix + "ProductStocks", InventoryDbProperties.DbSchema);

            b.ConfigureByConvention();

        });


        builder.Entity<OrderItemQuantity>(b =>
        {
            b.ToTable(InventoryDbProperties.DbTablePrefix + "OrderItemQuantities", InventoryDbProperties.DbSchema);

            b.ConfigureByConvention();

        });
    }
}
