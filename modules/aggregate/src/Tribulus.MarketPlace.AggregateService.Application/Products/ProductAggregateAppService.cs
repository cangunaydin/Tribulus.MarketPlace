
using Dapr.Actors;
using Dapr.Actors.Client;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.AggregateService.Actors;
using Tribulus.MarketPlace.AggregateService.Actors.Products;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
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
    private readonly ILogger<ProductAggregateAppService> _logger;
    private readonly IProductProcessActor _productProcessActor;

    public ProductAggregateAppService(
        IMediator mediatr,
        IRequestClient<CreateProduct> createProductRequestClient,
        IProductStockAppService productStockAppService,
        IProductPriceAppService productPriceAppService,
        IProductAppService productAppService,
        ILogger<ProductAggregateAppService> logger
        )
    {
        _mediatr = mediatr;
        _createProductRequestClient = createProductRequestClient;
        _productStockAppService = productStockAppService;
        _productPriceAppService = productPriceAppService;
        _productAppService = productAppService;
        _logger = logger;
    }

    public async Task<ProductAggregateDto> CreateAsync(CreateProductAggregateDto input)
    {
        try
        {
            var id = GuidGenerator.Create();
            _logger.LogInformation("***DaprClient CreateAsync***: " + input);

            var _productProcessActor = GetProductProcessActor(id);
            await _productProcessActor.Submit(id, input);

            return new ProductAggregateDto
            {
                Id = id,
                Name = input.Name,
                Description = input.Description,
                StockCount = input.StockCount,
                Price = input.Price
            };
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"***----DaprClient CreateAsync Exception----***: {ex.Message}");
            throw new UserFriendlyException(ex.Message);
        }
        //try
        //{
        //    var id = GuidGenerator.Create();
        //    Response response = await _createProductRequestClient.GetResponse<ProductCreated, ProductCreationFaulted>(new
        //    {
        //        ProductId = id,
        //        Name = input.Name,
        //        Description = input.Description,
        //        StockCount = input.StockCount,
        //        Price = input.Price,
        //        UserId = CurrentUser.Id

        //    });

        //    return response switch
        //    {
        //        (_, ProductCreated completed) => completed.Product,
        //        (_, ProductCreationFaulted notCompleted) => throw new UserFriendlyException(notCompleted.Reason),
        //        _ => throw new Exception("unknown error has happened")
        //    };
        //}
        //catch (RequestTimeoutException)
        //{
        //    throw new UserFriendlyException("Timeout has expired");
        //}
    }

    private IProductProcessActor GetProductProcessActor(Guid productId)
    {
        var actorType = nameof(ProductProcessActor);
        var actorId = new ActorId(productId.ToString());
        _logger.LogInformation($"GetProductProcessActor****: {actorId}");
        var _productProcessActor = ActorProxy.Create<IProductProcessActor>(actorId, actorType);
        return _productProcessActor;
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

            _logger.LogInformation("***DaprClient inventoryResult***: " + inventoryResult.StockCount);
            _logger.LogInformation("***DaprClient marketingResult***: " + marketingResult.Name);
            _logger.LogInformation("***DaprClient marketingResult***: " + marketingResult.Description);
            _logger.LogInformation("***DaprClient salesResult***: " + salesResult.Price);
            ObjectMapper.Map<ProductStockDto, ProductAggregateDto>(inventoryResult, productDetailEto.Product);
            ObjectMapper.Map<ProductDto, ProductAggregateDto>(marketingResult, productDetailEto.Product);
            ObjectMapper.Map<ProductPriceDto, ProductAggregateDto>(salesResult, productDetailEto.Product);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("***GetAsync Exception***: " + ex);
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
