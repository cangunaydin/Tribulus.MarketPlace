using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore;

[ConnectionStringName(SalesDbProperties.ConnectionStringName)]
public interface ISalesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
