using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Shipping.Permissions;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public class ProductDeliveryAppService : AdminShippingAppService, IProductDeliveryAppService
    {
        private readonly IRepository<ProductDelivery, Guid> _productDeliveryRepository;

        public ProductDeliveryAppService(IRepository<ProductDelivery, Guid> productDeliveryRepository)
        {
            _productDeliveryRepository = productDeliveryRepository;
        }

        [Authorize(ShippingPermissions.ProductDeliveries.Create)]
        public async Task<ProductDeliveryDto> CreateAsync(Guid id, CreateProductDeliveryDto input)
        {
            var productDelivery = new ProductDelivery(id, input.ShippingName, input.MinDays, input.MaxDays); //todo change the guid generation

            await _productDeliveryRepository.InsertAsync(productDelivery, true);
            return ObjectMapper.Map<ProductDelivery, ProductDeliveryDto>(productDelivery);
        }

        public async Task<ProductDeliveryDto> GetAsync(Guid id)
        {
            var productStock = await _productDeliveryRepository.GetAsync(id);
            return ObjectMapper.Map<ProductDelivery, ProductDeliveryDto>(productStock);
        }

        public async Task<ListResultDto<ProductDeliveryDto>> GetListAsync(ProductDeliveryFilterDto input)
        {
            var query = await _productDeliveryRepository.GetQueryableAsync();
            query = query.WhereIf(!input.Ids.IsNullOrEmpty(), o => input.Ids.Contains(o.Id));

            var productDeliveries = await AsyncExecuter.ToListAsync(query);
            var productDeliveriesDto = ObjectMapper.Map<List<ProductDelivery>, List<ProductDeliveryDto>>(productDeliveries);

            return new ListResultDto<ProductDeliveryDto>(productDeliveriesDto);
        }

        [Authorize(ShippingPermissions.ProductDeliveries.Update)]
        public async Task UpdateAsync(Guid id, UpdateProductDeliveryDto input)
        {
            var productStock = await _productDeliveryRepository.GetAsync(id);
            productStock.UpdateMaxDays(input.MaxDays);
            productStock.UpdateMinDays(input.MinDays);
            productStock.UpdateShippingName(input.ShippingName);
            await _productDeliveryRepository.UpdateAsync(productStock);
        }
    }
}

