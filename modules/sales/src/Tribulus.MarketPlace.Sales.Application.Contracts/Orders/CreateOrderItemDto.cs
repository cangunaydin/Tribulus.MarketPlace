using System;

namespace Tribulus.MarketPlace.Sales.Orders;

public class CreateOrderItemDto
{
    public Guid ProductId { get;  set; }

    public decimal Price { get;  set; }

    public int Quantity { get;  set; }
}
