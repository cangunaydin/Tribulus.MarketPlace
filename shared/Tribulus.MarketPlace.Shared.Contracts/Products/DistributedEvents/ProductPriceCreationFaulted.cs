using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface ProductPriceCreationFaulted
{
    public Guid Id { get; }
}
