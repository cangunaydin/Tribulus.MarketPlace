using System;

namespace Tribulus.MarketPlace.Admin.Messages
{
    public interface InventoryProductArgument
    {
        public Guid ProductId { get; set; }
        public int StockCount { get; set; }
    }
}
