using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Inventory
{
    public interface IProductStockAppService : IApplicationService
    {
        Task<ListResultDto<ProductStockDto>> GetListAsync(ProductStockListFilterDto input);

    }
}
