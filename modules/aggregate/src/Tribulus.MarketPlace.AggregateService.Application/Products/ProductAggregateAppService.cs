﻿
using MassTransit;
using MediatR;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Tribulus.MarketPlace.AggregateService.Products.Events;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Products.LocalEvents;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.AggregateService.Products;

public class ProductAggregateAppService : AggregateServiceAppService, IProductAggregateAppService
{
    private readonly IMediator _mediatr;
    private readonly IRequestClient<CreateProduct> _createProductRequestClient;
    public ProductAggregateAppService(
        IMediator mediatr, IRequestClient<CreateProduct> createProductRequestClient)
    {
        _mediatr = mediatr;
        _createProductRequestClient = createProductRequestClient;
    }

    public async Task<ProductAggregateDto> CreateAsync(CreateProductAggregateDto input)
    {
        try
        {
            var id = GuidGenerator.Create();
            Response response = await _createProductRequestClient.GetResponse<ProductCreated, ProductCreationFaulted>(new
            {
                ProductId = id,
                Name = input.Name,
                Description = input.Description,
                StockCount = input.StockCount,
                Price = input.Price,
                UserId = CurrentUser.Id

            });

            return response switch
            {
                (_, ProductCreated completed) => completed.Product,
                (_, ProductCreationFaulted notCompleted) => throw new UserFriendlyException(notCompleted.Reason),
                _ => throw new Exception("unknown error has happened")
            };
        }
        catch (RequestTimeoutException)
        {
            throw new UserFriendlyException("Timeout has expired");
        }
    }

    public async Task<ProductAggregateDto> GetAsync(Guid id)
    {
        var productDetailEto = new GetProductDetailEto()
        {
            Id = id,
            Product = new ProductAggregateDto()
        };
        await _mediatr.Publish(productDetailEto);
        return productDetailEto.Product;
    }



    public async Task<PagedResultDto<ProductAggregateDto>> GetProducts(ProductFilterDto input)
    {

        var productListEto = new GetProductListEto()
        {
            Filter = input,
            Products = null
        };
        await _mediatr.Publish(productListEto);
        return productListEto.Products;
    }
}