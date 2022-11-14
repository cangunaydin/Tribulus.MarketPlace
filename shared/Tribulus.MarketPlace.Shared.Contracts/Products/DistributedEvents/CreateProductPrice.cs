using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface CreateProductPrice
{
    public Guid Id { get; }

    public decimal Price { get; }
}
