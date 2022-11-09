using System;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public interface ProductStockArguments
    {
        public Guid ProductId { get; }
        public int StockCount { get; }

        public Guid UserId { get; }

        public int? TenantId { get; }
    }
}
