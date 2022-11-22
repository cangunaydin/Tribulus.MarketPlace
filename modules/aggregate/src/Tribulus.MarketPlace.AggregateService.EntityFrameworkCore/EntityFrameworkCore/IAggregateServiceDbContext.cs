using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.AggregateService.EntityFrameworkCore;

[ConnectionStringName(AggregateServiceDbProperties.ConnectionStringName)]
public interface IAggregateServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
