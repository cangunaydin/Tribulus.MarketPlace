using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Orders;

namespace Tribulus.MarketPlace.EntityFrameworkCore
{
    public static class MarketPlaceDbContextModelCreatingExtensions
    {
        public static void ConfigureMarketPlace(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<Product>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "Products", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConsts.MaxNameLength);
                b.Property(x => x.Description).HasMaxLength(ProductConsts.MaxDescriptionLength);

                b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.OwnerUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);

                b.HasIndex(x => x.Name);
            });

            builder.Entity<Order>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "Orders", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConsts.MaxNameLength);

                b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.OwnerUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);

                b.HasIndex(x => x.Name);

                b.HasMany(x => x.OrderItems).WithOne().IsRequired().HasForeignKey(x => x.OrderId);
            });

            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(MarketPlaceConsts.DbTablePrefix + "OrderItems", MarketPlaceConsts.DbSchema);

                b.ConfigureByConvention(); 
                
                b.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}
