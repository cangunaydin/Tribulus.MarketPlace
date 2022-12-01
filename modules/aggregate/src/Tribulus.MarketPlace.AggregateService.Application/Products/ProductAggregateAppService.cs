using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
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
    private readonly IProductStockAppService _productStockAppService;
    private readonly IProductPriceAppService _productPriceAppService;
    private readonly IProductAppService _productAppService;

    public ProductAggregateAppService(
        IMediator mediatr,
        IRequestClient<CreateProduct> createProductRequestClient,
        IProductStockAppService productStockAppService,
        IProductPriceAppService productPriceAppService,
        IProductAppService productAppService)
    {
        _mediatr = mediatr;
        _createProductRequestClient = createProductRequestClient;
        _productStockAppService = productStockAppService;
        _productPriceAppService = productPriceAppService;
        _productAppService = productAppService;
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

        try
        {
            var inventoryResult = await _productStockAppService.GetAsync(id);
            var marketingResult = await _productAppService.GetAsync(id);
            var salesResult = await _productPriceAppService.GetAsync(id);

            Logger.LogInformation("***DaprClient inventoryResult***: " + inventoryResult.StockCount);
            Logger.LogInformation("***DaprClient marketingResult***: " + marketingResult.Name);
            Logger.LogInformation("***DaprClient marketingResult***: " + marketingResult.Description);
            Logger.LogInformation("***DaprClient salesResult***: " + salesResult.Price);
            ObjectMapper.Map<ProductStockDto, ProductAggregateDto>(inventoryResult, productDetailEto.Product);
            ObjectMapper.Map<ProductDto, ProductAggregateDto>(marketingResult, productDetailEto.Product);
            ObjectMapper.Map<ProductPriceDto, ProductAggregateDto>(salesResult, productDetailEto.Product);
        }
        catch (Exception ex)
        {
            Logger.LogInformation("***GetAsync Exception***: " + ex);
        }

        //var productDetailEto = new GetProductDetailEto()
        //{
        //    Id = id,
        //    Product = new ProductAggregateDto()
        //};


        //await _mediatr.Publish(productDetailEto);
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
