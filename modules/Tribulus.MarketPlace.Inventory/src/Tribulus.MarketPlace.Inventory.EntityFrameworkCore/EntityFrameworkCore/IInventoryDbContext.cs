using Microsoft.EntityFrameworkCore;
using Tribulus.MarketPlace.Inventory.Orders;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore;

[ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
public interface IInventoryDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<ProductStock> ProductStocks { get; set; }
    DbSet<OrderItemQuantity> OrderItemQuantities { get; set; }
}
