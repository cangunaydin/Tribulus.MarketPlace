

using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Marketing;

public interface IProductCompositionService 
{
    Task<ProductViewModelCompositionDto> GetAsync(Guid id);

    Task<ProductListDto> GetProducts(ProductFilterDto input);
}
