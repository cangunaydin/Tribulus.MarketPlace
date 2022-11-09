using MassTransit.Courier;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Sales.Products;

public class CreateProductPriceActivity :
        ICreateProductPriceActivity
{
    private readonly IRepository<ProductPrice, Guid> _productPriceRepository;
    private readonly IObjectMapper _objectMapper;
    public CreateProductPriceActivity(IRepository<ProductPrice, Guid> productPriceRepository, IObjectMapper objectMapper)
    {
        _productPriceRepository = productPriceRepository;
        _objectMapper = objectMapper;
    }

    public async Task<CompensationResult> Compensate(CompensateContext<ProductPriceLog> context)
    {
        await _productPriceRepository.DeleteAsync(context.Log.ProductId);
        return context.Compensated();
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ProductPriceArguments> context)
    {
        var productPrice = new ProductPrice(context.Arguments.ProductId, context.Arguments.Price);
        await _productPriceRepository.InsertAsync(productPrice, true);
        var productPriceDto=_objectMapper.Map<ProductPrice,ProductPriceDto>(productPrice);

        return context.CompletedWithVariables<ProductPriceLog>(new { ProductId = productPrice.Id }, new { ProductPrice= productPriceDto });
    }
}
