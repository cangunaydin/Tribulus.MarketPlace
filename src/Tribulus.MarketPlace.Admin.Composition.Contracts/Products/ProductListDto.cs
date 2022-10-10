using System.Collections.Generic;

namespace Tribulus.MarketPlace.Admin.Products;

public class ProductListDto 
{
    public ProductListDto()
    {
        Products = new List<ProductCompositionDto>();
    }

    public List<ProductCompositionDto> Products { get; set; }
}
