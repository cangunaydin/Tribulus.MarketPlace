using System.Collections.Generic;

namespace Tribulus.MarketPlace.Admin.Products;

public class ProductListDto 
{
    public ProductListDto()
    {
        Products = new List<ProductViewModelCompositionDto>();
    }

    public List<ProductViewModelCompositionDto> Products { get; set; }
}
