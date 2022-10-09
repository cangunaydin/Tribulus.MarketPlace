using System;
using System.Collections.Generic;

namespace Tribulus.MarketPlace.Inventory.Events
{
    public class StockNotAvailableEto
    {
        public Guid OrderId { get; set; }

        public List<Guid> ProductIds { get; set; }
    }
}
