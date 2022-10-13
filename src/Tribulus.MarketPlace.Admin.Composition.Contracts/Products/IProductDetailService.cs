using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;

public interface IProductDetailService
{
    Task<ProductCompositionDto> GetAsync(Guid id);
}

