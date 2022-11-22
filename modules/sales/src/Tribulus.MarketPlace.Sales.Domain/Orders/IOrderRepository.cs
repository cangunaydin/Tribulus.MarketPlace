using System;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Sales.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
