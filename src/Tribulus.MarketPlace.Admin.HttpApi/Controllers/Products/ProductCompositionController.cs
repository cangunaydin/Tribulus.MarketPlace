﻿
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Localization;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Products.LocalEvents;
using Tribulus.MarketPlace.Products.MassTransit.Commands;
using Tribulus.MarketPlace.Products.MassTransit.Events;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Controllers;

[ControllerName("ProductComposition")]
[Route("api/composition/product")]
public class ProductCompositionController : AdminController, IProductCompositionService
{
    private readonly IMediator _mediator;
    private readonly IRequestClient<CreateProduct> _createProductRequestClient;
    public ProductCompositionController(
        IMediator mediator, IRequestClient<CreateProduct> createProductRequestClient)
    {
        LocalizationResource = typeof(MarketPlaceResource);
        _mediator = mediator;
        _createProductRequestClient = createProductRequestClient;
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
    public async Task<ProductCompositionDto> CreateAsync(CreateProductCompositionDto input)
    {
        try
        {
            var id = GuidGenerator.Create();
            Response response = await _createProductRequestClient.GetResponse<ProductCreated, ProductCreationFaulted>(new
            {
                ProductId = id,
                Name=input.Name,
                Description=input.Description,
                StockCount=input.StockCount,
                Price=input.Price

            });

            return response switch
            {
                (_, ProductCreated completed) => new ProductCompositionDto
                {
                    Id= completed.Product.Id,
                    Description=completed.Product.Description,
                    Name=completed.Product.Name,
                    Price=completed.Product.Price,
                    StockCount=completed.Product.StockCount
                },
                (_, ProductCreationFaulted notCompleted) => throw new UserFriendlyException(notCompleted.Reason),
                _ => throw new Exception("unknown error has happened")
            };
        }
        catch (RequestTimeoutException)
        {
            throw new UserFriendlyException("Timeout has expired");
        }
    }
}
