using MassTransit;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.AggregateService.Products.Activities
{ 
public class CreateProductPriceActivity :
        ICreateProductPriceActivity
{
    private readonly IProductPriceAppService _productPriceAppService;
    private readonly IObjectMapper _objectMapper;
    public CreateProductPriceActivity(IProductPriceAppService productPriceAppService, IObjectMapper objectMapper)
    {
        _productPriceAppService = productPriceAppService;
        _objectMapper = objectMapper;
    }

    public async Task<CompensationResult> Compensate(CompensateContext<ProductPriceLog> context)
    {
        await _productPriceAppService.DeleteAsync(context.Log.ProductId);
        return context.Compensated();
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ProductPriceArguments> context)
    {
        var createProductPriceDto = new CreateProductPriceDto { Price = context.Arguments.Price };
        var productPriceDto = await _productPriceAppService.CreateAsync(context.Arguments.ProductId, createProductPriceDto);

        return context.CompletedWithVariables<ProductPriceLog>(new { ProductId = productPriceDto.Id }, new { ProductPrice= productPriceDto });
    }
}
}

