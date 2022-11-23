using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.AggregateService.Products.Events;

public interface ProductCreated
{
    public ProductAggregateDto Product { get; }
}
