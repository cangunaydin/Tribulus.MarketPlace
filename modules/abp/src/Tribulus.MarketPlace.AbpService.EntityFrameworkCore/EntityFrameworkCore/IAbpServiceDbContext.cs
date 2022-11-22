using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.AbpService.EntityFrameworkCore;

[ConnectionStringName(AbpServiceDbProperties.ConnectionStringName)]
public interface IAbpServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
