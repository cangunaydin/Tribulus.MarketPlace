using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;

[ConnectionStringName(AggregateServiceDbProperties.ConnectionStringName)]
public class AggregateServiceDbContext : AbpDbContext<AggregateServiceDbContext>, IAggregateServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public AggregateServiceDbContext(DbContextOptions<AggregateServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAggregateService();
    }
}
