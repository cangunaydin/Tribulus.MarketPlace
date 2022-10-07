using System;

namespace Tribulus.MarketPlace.Inventory.Events
{
    public class StockExistsEto
    {
        public Guid OrderId { get; set; }
    }
}
