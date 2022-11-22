using System;

namespace Tribulus.MarketPlace.Sales.Orders.Events;

public class OrderConfirmedEto
{
    public Guid OrderId { get; set; }
}
