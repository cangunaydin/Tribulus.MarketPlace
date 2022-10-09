using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Inventory.Products
{
    [Authorize(InventoryPermissions.ProductStocks.Default)]
    public class ProductStockAppService : InventoryAppService, IProductStockAppService
    {
        private readonly IRepository<ProductStock, Guid> _productStockRepository;

        public ProductStockAppService(IRepository<ProductStock, Guid> productStockRepository)
        {
            _productStockRepository = productStockRepository;
        }

      
        public async Task<ListResultDto<ProductStockDto>> GetListAsync(ProductStockListFilterDto input)
        {
            var query = await _productStockRepository.GetQueryableAsync();
            query = query.WhereIf(!input.Ids.IsNullOrEmpty(), o => input.Ids.Contains(o.Id));

            var productStocks = await AsyncExecuter.ToListAsync(query);
            var productStocksDto = ObjectMapper.Map<List<ProductStock>, List<ProductStockDto>>(productStocks);

            return new ListResultDto<ProductStockDto>(productStocksDto);
        }

    }
}
