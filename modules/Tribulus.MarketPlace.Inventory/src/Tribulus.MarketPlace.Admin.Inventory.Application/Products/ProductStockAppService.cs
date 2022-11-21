using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Permissions;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    [Authorize(InventoryPermissions.ProductStocks.Default)]
    public class ProductStockAppService : AdminInventoryAppService, IProductStockAppService
    {
        private readonly IRepository<ProductStock, Guid> _productStockRepository;

        public ProductStockAppService(IRepository<ProductStock, Guid> productStockRepository)
        {
            _productStockRepository = productStockRepository;
        }

        [Authorize(InventoryPermissions.ProductStocks.Create)]
        public async Task<ProductStockDto> CreateAsync(Guid id, CreateProductStockDto input)
        {
            var productStock = new ProductStock(id, input.StockCount); //todo change the guid generation
           
            await _productStockRepository.InsertAsync(productStock, true);
            return ObjectMapper.Map<ProductStock, ProductStockDto>(productStock);
        }

        [Authorize(InventoryPermissions.ProductStocks.Default)]
        public async Task<ProductStockDto> GetAsync(Guid id)
        {
            var productStock = await _productStockRepository.GetAsync(id);
            return ObjectMapper.Map<ProductStock, ProductStockDto>(productStock);
        }

        [Authorize(InventoryPermissions.ProductStocks.Default)]
        public async Task<ListResultDto<ProductStockDto>> GetListAsync(ProductStockListFilterDto input)
        {
            var query = await _productStockRepository.GetQueryableAsync();
            query = query.WhereIf(!input.Ids.IsNullOrEmpty(), o => input.Ids.Contains(o.Id));

            var productStocks = await AsyncExecuter.ToListAsync(query);
            var productStocksDto = ObjectMapper.Map<List<ProductStock>, List<ProductStockDto>>(productStocks);

            return new ListResultDto<ProductStockDto>(productStocksDto);
        }

        [Authorize(InventoryPermissions.ProductStocks.Update)]
        public async Task UpdateAsync(Guid id, UpdateProductStockDto input)
        {
            var productStock = await _productStockRepository.GetAsync(id);
            productStock.UpdateStockCount(input.StockCount);
            await _productStockRepository.UpdateAsync(productStock);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productStockRepository.DeleteAsync(id);
        }
    }
}
