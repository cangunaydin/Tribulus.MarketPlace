
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Events;
using Tribulus.MarketPlace.Admin.Products.Models;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Controllers;

[ControllerName("ProductComposition")]
[Route("api/composition/product")]
public class ProductCompositionController : AdminController, IProductCompositionService
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRequestClient<SubmitProduct> _client;

    public ProductCompositionController(IPublishEndpoint publishEndpoint,
        IRequestClient<SubmitProduct> client,
        IMediator mediator)
    {
        _publishEndpoint = publishEndpoint;
        LocalizationResource = typeof(MarketPlaceResource);
        _mediator = mediator;
        _client = client;
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
    public async Task<ActionResult> PostAsync([FromBody] ProductCompositionDto product)
    {
        //await _publishEndpoint.Publish<SubmitProductEvent>(new
        //{
        //    Name = product.Name,
        //    Description = product.Description,
        //    Price = product.Price,
        //    StockCount = product.StockCount,
        //    CorrelationId = Guid.NewGuid()
        //});
        //return Ok(product);

        try
        {
            Response response = await _client.GetResponse<SubmitProductCompleted, SubmitProductFaulted>(product);

            return response switch
            {
                (_, SubmitProductCompleted completed) => Ok(new
                {
                    completed.ProductId,
                }),
                (_, SubmitProductFaulted faulted) => BadRequest(new
                {
                    faulted.ProductId,
                }),
                _ => BadRequest()
            };
        }
        catch (RequestTimeoutException ex)
        {
            return Ok(ex.Message);
        }
    }
}
