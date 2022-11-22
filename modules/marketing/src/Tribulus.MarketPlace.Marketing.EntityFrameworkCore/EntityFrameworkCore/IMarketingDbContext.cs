using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore;

[ConnectionStringName(MarketingDbProperties.ConnectionStringName)]
public interface IMarketingDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Product> Products { get; set; }
}
