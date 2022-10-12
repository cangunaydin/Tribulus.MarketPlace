using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tribulus.MarketPlace.Shipping.EntityFrameworkCore;

public static class ShippingDbContextModelCreatingExtensions
{
    public static void ConfigureShipping(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example: */

        builder.Entity<ProductShippingOptions>(b =>
        {
            //Configure table & schema name
            b.ToTable(ShippingDbProperties.DbTablePrefix + "ProductShippingOptions", ShippingDbProperties.DbSchema);

            b.ConfigureByConvention();
        });
       
    }
}
