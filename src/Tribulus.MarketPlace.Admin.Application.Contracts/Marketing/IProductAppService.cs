using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Marketing
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetListAsync(ProductListFilterDto input);

        Task<ProductDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductDto input);

        Task<ProductDto> CreateAsync(CreateProductDto input);
    }
}
