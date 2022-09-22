using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
    }
}
