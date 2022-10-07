using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Permissions;
using Tribulus.MarketPlace.Sales;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Sales
{
    [Authorize]
    public class ProductPriceAppService : MarketPlaceAdminAppService, IProductPriceAppService
    {
        private readonly IRepository<ProductPrice, Guid> _productPriceRepository;

        public ProductPriceAppService(IRepository<ProductPrice, Guid> productPriceRepository)
        {
            _productPriceRepository = productPriceRepository;
        }

        [Authorize]
        public async Task<ProductPriceDto> CreateAsync(CreateProductPriceDto input)
        {
            var productPrice = new ProductPrice(GuidGenerator.Create(), input.Price);
            await _productPriceRepository.InsertAsync(productPrice, true);
            return ObjectMapper.Map<ProductPrice, ProductPriceDto>(productPrice);
        }

        [Authorize]
        public async Task<ProductPriceDto> GetAsync(Guid id)
        {
            var productPrice = await _productPriceRepository.GetAsync(id);
            return ObjectMapper.Map<ProductPrice, ProductPriceDto>(productPrice);
        }

        [Authorize]
        public async Task<ListResultDto<ProductPriceDto>> GetListAsync(ProductPriceListFilterDto input)
        {
            var query = await _productPriceRepository.GetQueryableAsync();
            query = query.WhereIf(!input.Ids.IsNullOrEmpty(), o => input.Ids.Contains(o.Id));

            var productPrices = await AsyncExecuter.ToListAsync(query);
            var productPricesDto = ObjectMapper.Map<List<ProductPrice>, List<ProductPriceDto>>(productPrices);

            return new ListResultDto<ProductPriceDto>( productPricesDto);
        }

        [Authorize]
        public async Task UpdateAsync(Guid id, UpdateProductPriceDto input)
        {
            var productPrice = await _productPriceRepository.GetAsync(id);
            productPrice.UpdatePrice(input.Price);
            await _productPriceRepository.UpdateAsync(productPrice);
        }
    }
}
