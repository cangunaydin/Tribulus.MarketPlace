using System;
using Tribulus.MarketPlace.Inventory.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore.Repositories
{
    public class OrderItemQuantityRepository : EfCoreRepository<IInventoryDbContext, OrderItemQuantity, Guid>, IOrderItemQuantityRepository
    {
        public OrderItemQuantityRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
