using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Products.Events
{
    public class StockCountChangedEventData
    {
        public Guid ProductId { get; set; }

        public int NewStockCount { get; set; }
    }
}
