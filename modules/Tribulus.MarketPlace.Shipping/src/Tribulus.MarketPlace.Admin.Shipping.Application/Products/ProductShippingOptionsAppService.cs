using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Shipping.Permissions;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{

    [Authorize(ShippingPermissions.ProductShippingOptions.Default)]
    public class ProductShippingOptionsAppService : AdminShippingAppService, IProductShippingOptionAppService
    {

        private readonly IRepository<ProductShippingOptions, Guid> _productShippingOptionRepository;

        public ProductShippingOptionsAppService(IRepository<ProductShippingOptions, Guid> productShippingOptionRepository)
        {
            _productShippingOptionRepository = productShippingOptionRepository;
        }


        public async Task<CreateProductShippingOptionDto> CreateAsync(Guid id, CreateProductShippingOptionDto input)
        {
            var productShippingOption= new ProductShippingOptions(id, input.ProductId,input.Option,input.EstimatedMinDeliveryDays,input.EstimatedMaxDeliveryDays); //todo change the guid generation
            await _productShippingOptionRepository.InsertAsync(productShippingOption, true);
            return ObjectMapper.Map<ProductShippingOptions, CreateProductShippingOptionDto>(productShippingOption);
        }
    }
}
