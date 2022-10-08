using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Sales;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tribulus.MarketPlace.EntityFrameworkCore
{
    public static class MarketPlaceDbContextModelCreatingExtensions
    {
        public static void ConfigureMarketPlace(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

           
            builder.Entity<ProductStock>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "ProductStocks", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention();

            });

            builder.Entity<ProductPrice>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "ProductPrices", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention();

            });

            builder.Entity<Order>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "Orders", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConsts.MaxNameLength);

                //b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.OwnerUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);

                b.HasIndex(x => x.Name);

                b.HasMany(x => x.OrderItems).WithOne().IsRequired().HasForeignKey(x => x.OrderId);
            });

            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "OrderItems", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention(); 
                
                b.HasOne<ProductPrice>().WithMany().HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<OrderItemQuantity>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "OrderItemQuantities", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention();
                
            });

        }
    }
}
