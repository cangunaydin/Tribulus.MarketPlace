using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Orders
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }



    }
}
