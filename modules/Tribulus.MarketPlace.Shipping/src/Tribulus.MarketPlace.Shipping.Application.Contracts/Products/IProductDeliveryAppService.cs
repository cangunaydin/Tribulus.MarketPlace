using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public interface IProductDeliveryAppService
    {
        Task<ListResultDto<ProductDeliveryDto>> GetListAsync(ProductDeliveryFilterDto input);

    }
}
