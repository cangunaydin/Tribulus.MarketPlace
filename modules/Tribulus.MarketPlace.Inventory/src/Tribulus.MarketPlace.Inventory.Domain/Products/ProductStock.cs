using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Inventory.Products
{
    public class ProductStock : FullAuditedAggregateRoot<Guid>
    {
        private ProductStock() { }

        public ProductStock(Guid id,
            int stockCount) : base(id)
        {

            UpdateStockCount(stockCount);
        }
        public void UpdateStockCount(int stockCount)
        {
            if (stockCount < 0)
                throw new ArgumentException(nameof(stockCount));

            StockCount = stockCount;

        }
        public int StockCount { get; private set; }
        public StockState StockState { get; private set; }
    }
}
