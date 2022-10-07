using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Permissions;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Admin.Inventory
{
    [Authorize]
    public class ProductStockAppService : MarketPlaceAdminAppService, IProductStockAppService
    {
        private readonly IRepository<ProductStock, Guid> _productStockRepository;

        public ProductStockAppService(IRepository<ProductStock, Guid> productStockRepository)
        {
            _productStockRepository = productStockRepository;
        }

        [Authorize]
        public async Task<ProductStockDto> CreateAsync(CreateProductStockDto input)
        {
            var productStock = new ProductStock(GuidGenerator.Create(),input.StockCount); //todo change the guid generation
           
            await _productStockRepository.InsertAsync(productStock, true);
            return ObjectMapper.Map<ProductStock, ProductStockDto>(productStock);
        }

        [Authorize]
        public async Task<ProductStockDto> GetAsync(Guid id)
        {
            var productStock = await _productStockRepository.GetAsync(id);
            return ObjectMapper.Map<ProductStock, ProductStockDto>(productStock);
        }

        [Authorize]
        public async Task<ListResultDto<ProductStockDto>> GetListAsync(ProductStockListFilterDto input)
        {
            var query = await _productStockRepository.GetQueryableAsync();
            query = query.WhereIf(!input.Ids.IsNullOrEmpty(), o => input.Ids.Contains(o.Id));

            var productStocks = await AsyncExecuter.ToListAsync(query);
            var productStocksDto = ObjectMapper.Map<List<ProductStock>, List<ProductStockDto>>(productStocks);

            return new ListResultDto<ProductStockDto>(productStocksDto);
        }

        [Authorize]
        public async Task UpdateAsync(Guid id, UpdateProductStockDto input)
        {
            var productStock = await _productStockRepository.GetAsync(id);
            productStock.UpdateStockCount(input.StockCount);
            await _productStockRepository.UpdateAsync(productStock);
        }
    }
}
