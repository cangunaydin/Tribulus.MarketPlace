using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore;

[ConnectionStringName(MarketingDbProperties.ConnectionStringName)]
public class MarketingDbContext : AbpDbContext<MarketingDbContext>, IMarketingDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Product> Products { get; set; }
    public MarketingDbContext(DbContextOptions<MarketingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMarketing();
    }
}
