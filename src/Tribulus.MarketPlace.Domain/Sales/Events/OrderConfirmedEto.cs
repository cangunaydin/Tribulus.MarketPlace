using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales;

namespace Tribulus.MarketPlace.Sales.Events
{
    public class OrderConfirmedEto
    {
        public Guid OrderId { get; set; }
    }
}
