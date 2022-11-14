using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Events;

public interface ProductCreated
{
    public ProductCompositionDto Product { get; }
}
