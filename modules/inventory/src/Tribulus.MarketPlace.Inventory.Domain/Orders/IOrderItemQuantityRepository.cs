using System;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Inventory.Orders
{
    public interface IOrderItemQuantityRepository : IRepository<OrderItemQuantity, Guid>
    {
    }
}
