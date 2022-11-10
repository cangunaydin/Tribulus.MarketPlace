using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface CreateProductStock
{
    public Guid Id { get; }

    public int StockCount { get; }
}
