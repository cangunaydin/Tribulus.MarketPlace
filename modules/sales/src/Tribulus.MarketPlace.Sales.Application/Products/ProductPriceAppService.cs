using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Sales.Products
{
    public class ProductPriceAppService : SalesAppService, IProductPriceAppService
    {
        private readonly IRepository<ProductPrice, Guid> _productPriceRepository;

        public ProductPriceAppService(IRepository<ProductPrice, Guid> productPriceRepository)
        {
            _productPriceRepository = productPriceRepository;
        }
        public async Task<ListResultDto<ProductPriceDto>> GetListAsync(ProductPriceListFilterDto input)
        {
            var query = await _productPriceRepository.GetQueryableAsync();
            query = query.WhereIf(!input.Ids.IsNullOrEmpty(), o => input.Ids.Contains(o.Id));

            var productPrices = await AsyncExecuter.ToListAsync(query);
            var productPricesDto = ObjectMapper.Map<List<ProductPrice>, List<ProductPriceDto>>(productPrices);

            return new ListResultDto<ProductPriceDto>(productPricesDto);
        }
    }
}
