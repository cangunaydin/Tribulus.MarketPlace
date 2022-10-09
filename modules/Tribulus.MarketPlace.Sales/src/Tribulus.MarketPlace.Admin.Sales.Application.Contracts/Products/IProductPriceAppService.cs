using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public interface IProductPriceAppService : IApplicationService
    {
        Task<ListResultDto<ProductPriceDto>> GetListAsync(ProductPriceListFilterDto input);

        Task<ProductPriceDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductPriceDto input);

        Task<ProductPriceDto> CreateAsync(CreateProductPriceDto input);
    }
}
