using System;

namespace Tribulus.MarketPlace.Sales.Orders.Events;

public class OrderPlacedEto
{
    public Guid OrderId { get; set; }
}
