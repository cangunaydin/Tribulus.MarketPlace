using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;

public interface IProductListService
{
    Task<ProductListDto> GetListAsync(ProductFilterDto input);
}

