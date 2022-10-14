using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Sales;

public interface IProductCompositionService
{
    Task<ProductViewModelCompositionDto> GetAsync(Guid id);

}
