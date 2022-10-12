using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Shipping.EntityFrameworkCore;

[ConnectionStringName(ShippingDbProperties.ConnectionStringName)]
public interface IShippingDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */

    DbSet<ProductShippingOptions> ProductShippingOptions { get; set; }
}
