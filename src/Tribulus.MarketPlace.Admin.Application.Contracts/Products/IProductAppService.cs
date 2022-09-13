using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetListAsync(ProductListFilterDto input);

        Task<ProductDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductDto input);

        Task<ProductDto> CreateAsync(CreateProductDto input);
    }
}
