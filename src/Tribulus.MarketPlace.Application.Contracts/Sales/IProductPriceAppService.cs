

using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Sales
{
    public interface IProductPriceAppService : IApplicationService
    {
        Task<ListResultDto<ProductPriceDto>> GetListAsync(ProductPriceListFilterDto input);

    }
}
