using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface ProductPriceCreated
{
    public Guid Id { get; }

    public decimal Price { get;}
}
