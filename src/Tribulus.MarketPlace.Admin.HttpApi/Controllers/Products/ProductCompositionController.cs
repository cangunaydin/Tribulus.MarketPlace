
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Events;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Controllers;

[ControllerName("ProductComposition")]
[Route("api/composition/product")]
public class ProductCompositionController : AdminController, IProductCompositionService
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductCompositionController(IPublishEndpoint publishEndpoint,
        IMediator mediator)
    {
        _publishEndpoint = publishEndpoint;
        LocalizationResource = typeof(MarketPlaceResource);
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductCompositionDto> GetAsync(Guid id)
    {
        var productDetailEto = new ProductDetailEto()
        {
            Id = id,
            Product = new ProductCompositionDto()
        };
        await _mediator.Publish(productDetailEto);
        return productDetailEto.Product;
    }

    [HttpGet]
    public async Task<PagedResultDto<ProductCompositionDto>> GetProducts(ProductFilterDto input)
    {
        var productListEto = new ProductListEto()
        {
            Filter = input,
            Products = null
        };
        await _mediator.Publish(productListEto);
        return productListEto.Products;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] ProductCompositionDto input)
    {
        await _publishEndpoint.Publish<SubmitProductEvent>(new
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            StockCount = input.StockCount,
            CorrelationId = Guid.NewGuid()
        });
        return Ok(input);    
    }
}
