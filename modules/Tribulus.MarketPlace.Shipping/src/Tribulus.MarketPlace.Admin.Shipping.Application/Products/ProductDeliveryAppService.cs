using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public class ProductDeliveryAppService : AdminShippingAppService, IProductDeliveryAppService
    {
        public Task<ProductDeliveryDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ListResultDto<ProductDeliveryDto>> GetListAsync(ProductDeliveryFilterDto input)
        {
            throw new NotImplementedException();
        }
    }
}
