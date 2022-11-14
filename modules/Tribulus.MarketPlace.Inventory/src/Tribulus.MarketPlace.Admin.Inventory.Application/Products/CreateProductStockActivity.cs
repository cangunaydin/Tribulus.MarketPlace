
using MassTransit.Courier;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Inventory.Products;

public class CreateProductStockActivity :
    ICreateProductStockActivity
{
    private readonly IRepository<ProductStock, Guid> _productStockRepository;
    private readonly IObjectMapper _objectMapper;
    public CreateProductStockActivity(IRepository<ProductStock, Guid> productStockRepository, 
        IObjectMapper objectMapper)
    {
        _productStockRepository = productStockRepository;
        _objectMapper = objectMapper;
    }
    public async Task<CompensationResult> Compensate(CompensateContext<ProductStockLog> context)
    {
        await _productStockRepository.DeleteAsync(context.Log.ProductId);
        return context.Compensated();
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<ProductStockArguments> context)
    {
        var productStock = new ProductStock(context.Arguments.ProductId, context.Arguments.StockCount);
        await _productStockRepository.InsertAsync(productStock, true);
        var productStockDto=_objectMapper.Map<ProductStock, ProductStockDto>(productStock);
        return context.CompletedWithVariables<ProductStockLog>(new { ProductId=productStock.Id }, new { ProductStock= productStockDto });
    }
}
