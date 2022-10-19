using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductDeliveryAppService : ShippingAppService, IProductDeliveryAppService
    {
        private readonly IRepository<ProductDelivery, Guid> _productDeliveryRepository;

        public async Task<ListResultDto<ProductDeliveryDto>> GetListAsync(ProductDeliveryFilterDto input)
        {
            throw new NotImplementedException();
        }
    }
}
