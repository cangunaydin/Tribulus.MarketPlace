

using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Sales
{
    public interface IProductPriceAppService : IApplicationService
    {
        Task<ListResultDto<ProductPriceDto>> GetListAsync(ProductPriceListFilterDto input);

        Task<ProductPriceDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateProductPriceDto input);

        Task<ProductPriceDto> CreateAsync(CreateProductPriceDto input);
    }
}
