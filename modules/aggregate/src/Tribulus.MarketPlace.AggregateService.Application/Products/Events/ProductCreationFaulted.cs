using System;

namespace Tribulus.MarketPlace.AggregateService.Products.Events;

public interface ProductCreationFaulted
{
    public Guid ProductId { get; }

    public string Reason { get; }

}
