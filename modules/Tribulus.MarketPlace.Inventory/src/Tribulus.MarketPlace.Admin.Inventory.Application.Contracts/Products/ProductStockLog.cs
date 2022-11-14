using System;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public interface ProductStockLog
    {
        public Guid ProductId { get; }

        public int StockCount { get; }
    }
}
