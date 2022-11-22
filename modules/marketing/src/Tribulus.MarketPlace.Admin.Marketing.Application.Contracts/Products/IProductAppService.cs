using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetListAsync(ProductListFilterDto input);

        Task<ProductDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductDto input);

        Task<ProductDto> CreateAsync(Guid id,CreateProductDto input);

        Task DeleteAsync(Guid id);
    }
}
