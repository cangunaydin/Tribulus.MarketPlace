using MassTransit;
using System.Threading.Tasks;
using System;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.ObjectMapping;
using Tribulus.MarketPlace.Admin.Inventory;
namespace Tribulus.MarketPlace.AggregateService.Products.Activities
{
    public class CreateProductStockActivity :
    IActivity<ProductStockArguments, ProductStockLog>, IExecuteActivity<ProductStockArguments>, IExecuteActivity, ICompensateActivity<ProductStockLog>, ICompensateActivity, IActivity
{
    private readonly IProductStockAppService _productStockAppService;
    private readonly IObjectMapper _objectMapper;
    public CreateProductStockActivity(IProductStockAppService productStockAppService,
        IObjectMapper objectMapper)
    {
        _productStockAppService = productStockAppService;
        _objectMapper = objectMapper;
    }
    public async Task<CompensationResult> Compensate(CompensateContext<ProductStockLog> context)
    {
        await _productStockAppService.DeleteAsync(context.Log.ProductId);
        return context.Compensated();
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<ProductStockArguments> context)
    {
        var asd = _productStockAppService.GetListAsync(new ProductStockListFilterDto { MaxResultCount = 10 });
        var productStock = new CreateProductStockDto { StockCount = context.Arguments.StockCount};
        try
        {
            var productStockDto = await _productStockAppService.CreateAsync(context.Arguments.ProductId, productStock);
            return context.CompletedWithVariables<ProductStockLog>(new { ProductId = context.Arguments.ProductId }, new { ProductStock = productStockDto });

        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
}