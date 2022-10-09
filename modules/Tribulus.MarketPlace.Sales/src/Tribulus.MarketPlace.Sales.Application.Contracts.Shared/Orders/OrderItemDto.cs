using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Sales.Orders;

public class OrderItemDto
{
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }
}
