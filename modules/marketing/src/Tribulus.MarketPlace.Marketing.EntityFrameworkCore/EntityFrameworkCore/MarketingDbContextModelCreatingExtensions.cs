using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore;

public static class MarketingDbContextModelCreatingExtensions
{
    public static void ConfigureMarketing(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure your own tables/entities inside here */
        builder.Entity<Product>(b =>
        {
            b.ToTable(MarketingDbProperties.DbTablePrefix + "Products", MarketingDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(ProductConsts.MaxDescriptionLength);


            b.HasIndex(x => x.Name);
        });
    }
}
