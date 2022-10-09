using System;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Inventory.Products
{
    public interface IProductStockRepository : IRepository<ProductStock, Guid>
    {
    }
}
