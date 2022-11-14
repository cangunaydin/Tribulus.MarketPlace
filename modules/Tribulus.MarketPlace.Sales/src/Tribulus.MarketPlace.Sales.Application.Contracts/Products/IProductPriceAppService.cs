using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Sales.Products
{
    public interface IProductPriceAppService : IApplicationService
    {
        Task<ListResultDto<ProductPriceDto>> GetListAsync(ProductPriceListFilterDto input);
    }
}
