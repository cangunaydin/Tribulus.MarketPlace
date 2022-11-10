using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface ProductCreated
{
    public Guid Id { get; }

    public string Name { get; }

    public string Description { get; }
}
