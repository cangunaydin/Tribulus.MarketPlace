using System;
using Tribulus.MarketPlace.Inventory.Events;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Inventory
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
            //ADD an EVENT TO BE PUBLISHED
            //AddLocalEvent(
            //    new StockCountChangedEto
            //    {
            //        ProductId = Id,
            //        NewStockCount = StockCount
            //    }
            //);

        }
        public int StockCount { get; private set; }
        public StockState StockState { get; private set; }
    }
}
