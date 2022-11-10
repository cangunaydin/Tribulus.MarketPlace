using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface ProductCreationFaulted
{
    public Guid Id { get; }
}
