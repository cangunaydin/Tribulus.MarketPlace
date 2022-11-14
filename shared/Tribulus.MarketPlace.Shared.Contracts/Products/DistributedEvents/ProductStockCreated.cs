using System;

namespace Tribulus.MarketPlace.Products.DistributedEvents;

public interface ProductStockCreated
{
    public Guid Id { get; }

    public int StockCount { get; }
}
