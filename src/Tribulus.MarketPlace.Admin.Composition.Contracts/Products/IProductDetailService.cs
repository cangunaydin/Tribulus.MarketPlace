using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;

public interface IProductCompositionService 
{
    Task<ProductCompositionDto> GetAsync(Guid id);

    Task<ProductListDto> GetProducts(ProductFilterDto input);
}

