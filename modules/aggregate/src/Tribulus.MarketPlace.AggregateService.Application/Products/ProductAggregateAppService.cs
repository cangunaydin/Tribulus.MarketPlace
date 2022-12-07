
using MassTransit;
using MediatR;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Inventory.Products.Command;
using Tribulus.MarketPlace.Admin.Marketing.Products.Command;
using Tribulus.MarketPlace.Admin.Sales.Products.Command;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Tribulus.MarketPlace.AggregateService.Products.Events;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Products.LocalEvents;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.AggregateService.Products;

public class ProductAggregateAppService : AggregateServiceAppService, IProductAggregateAppService
{
    private readonly IMediator _mediatr;
    private readonly IRequestClient<CreateProduct> _createProductRequestClient;
    private readonly IRequestClient<GetProductRequest> _getProductRequestClient;
    private readonly IRequestClient<GetProductStockRequest> _getProductStockRequestClient;
    private readonly IRequestClient<GetProductPriceRequest> _getProductPriceRequestClient;


    public ProductAggregateAppService(
        IMediator mediatr, IRequestClient<CreateProduct> createProductRequestClient, IRequestClient<GetProductRequest> getProductRequestClient, IRequestClient<GetProductStockRequest> getProductStockRequestClient, IRequestClient<GetProductPriceRequest> getProductPriceRequestClient)
    {
        _mediatr = mediatr;
        _getProductRequestClient = getProductRequestClient;
        _createProductRequestClient = createProductRequestClient;
        _getProductStockRequestClient = getProductStockRequestClient;
        _getProductPriceRequestClient = getProductPriceRequestClient;   

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


    public async Task<ProductAggregateDto> GetRequestReplyAsync(Guid id)
    {

        var prodresponse =  _getProductRequestClient.GetResponse<ProductDto>(new { ProductId = id });

        var prodstockresponse = _getProductStockRequestClient.GetResponse<ProductStockDto>(new { ProductId = id });

        var prodpriceresponse = _getProductPriceRequestClient.GetResponse<ProductPriceDto>(new { ProductId = id });

       

        await Task.WhenAll(prodresponse, prodstockresponse, prodpriceresponse);

        var prod = await prodresponse;
        var prodstock = await prodstockresponse;
        var prodprice = await prodpriceresponse;

        return new ProductAggregateDto
        {
            Id = prod.Message.Id,
            Name = prod.Message.Name,
            Description = prod.Message?.Description,
            StockCount = prodstock.Message.StockCount,
            Price = prodprice.Message.Price
        };
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
