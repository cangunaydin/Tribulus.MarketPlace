using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface ProductStockCreationFaulted
{
    public Guid Id { get; }
}
