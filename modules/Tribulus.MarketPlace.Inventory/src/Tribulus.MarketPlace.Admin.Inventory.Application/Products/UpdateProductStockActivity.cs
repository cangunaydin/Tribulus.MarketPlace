using MassTransit;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public class UpdateProductStockActivity :
    IUpdateProductStockActivity
    {
        private readonly IRepository<ProductStock, Guid> _productStockRepository;
        private readonly IObjectMapper _objectMapper;
        public UpdateProductStockActivity(IRepository<ProductStock, Guid> productStockRepository,
            IObjectMapper objectMapper)
        {
            _productStockRepository = productStockRepository;
            _objectMapper = objectMapper;
        }
        public async Task<CompensationResult> Compensate(CompensateContext<ProductStockLog> context)
        {
            var productStock = await _productStockRepository.GetAsync(context.Log.ProductId);
            productStock.UpdateStockCount(context.Log.StockCount);
            await _productStockRepository.UpdateAsync(productStock);
            return context.Compensated();
        }
        public async Task<ExecutionResult> Execute(ExecuteContext<ProductStockArguments> context)
        {
            var productStock = await _productStockRepository.GetAsync(context.Arguments.ProductId);
            productStock.UpdateStockCount(context.Arguments.StockCount);
            await _productStockRepository.UpdateAsync(productStock);
            var productStockDto = _objectMapper.Map<ProductStock, ProductStockDto>(productStock);

            return context.CompletedWithVariables<ProductStockLog>(new { ProductId = productStock.Id }, new { ProductStock = productStockDto });
        }
    }

}
