
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Tribulus.MarketPlace.AggregateService.Products.Events;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.AggregateService.Products;

public class ProductAggregateAppService : AggregateServiceAppService, IProductAggregateAppService
{
    private readonly IRequestClient<CreateProduct> _createProductRequestClient;
    private readonly IProductStockAppService _productStockAppService;
    private readonly IProductAppService _productAppService;
    private readonly IProductPriceAppService _productPriceAppService;

    public ProductAggregateAppService(
        IRequestClient<CreateProduct> createProductRequestClient,
        IProductStockAppService productStockAppService,
        IProductAppService productAppService,
        IProductPriceAppService productPriceAppService)
    {
        _createProductRequestClient = createProductRequestClient;
        _productStockAppService = productStockAppService;
        _productAppService = productAppService;
        _productPriceAppService = productPriceAppService;
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
        var productAggregate = new ProductAggregateDto();
        var productTask = _productAppService.GetAsync(id);
        var productPriceTask = _productPriceAppService.GetAsync(id);
        var productStockTask = _productStockAppService.GetAsync(id);
        

        await Task.WhenAll(productTask, productStockTask, productPriceTask);
        ObjectMapper.Map(productTask.Result, productAggregate);
        ObjectMapper.Map(productPriceTask.Result, productAggregate);
        ObjectMapper.Map(productStockTask.Result, productAggregate);
        
        return productAggregate;
    }
   


    public async Task<PagedResultDto<ProductAggregateDto>> GetProducts(ProductAggregateFilterDto input)
    {
        //map the input to product list filter to get all products from marketing.
        //marketing is the first service we need to get the products from.
        //since it owns the name and search is gonna be through name.
        var aggregateFilter = ObjectMapper.Map<ProductAggregateFilterDto, ProductListFilterDto>(input);
        var productsResult=await _productAppService.GetListAsync(aggregateFilter);
        var products = productsResult.Items;

        //get the ids that comes after filter.
        var productIds = products.Select(o => o.Id).ToList();
        //get the prices and stock counts afterwards.
        var productStockTask = _productStockAppService.GetListAsync(new ProductStockListFilterDto() { Ids = productIds });
        var productPriceTask = _productPriceAppService.GetListAsync(new ProductPriceListFilterDto() { Ids = productIds });

        //wait for the tasks to be finished
        await Task.WhenAll(productStockTask, productPriceTask);

        //get results
        var productPrices = productPriceTask.Result.Items;
        var productStocks= productStockTask.Result.Items;



        //create a list for returned dto.
        var productAggregates= products.Select(ObjectMapper.Map<ProductDto,ProductAggregateDto>).ToList();
        foreach (var productAggregate in productAggregates)
        {
            //fetch the data to compose.
            var productPrice = productPrices.Where(o => o.Id == productAggregate.Id).First();
            var productStock=productStocks.Where(o=>o.Id== productAggregate.Id).First();
            //map it.
            ObjectMapper.Map(productPrice, productAggregate);
            ObjectMapper.Map(productStock, productAggregate);
        }
        return new PagedResultDto<ProductAggregateDto>(productsResult.TotalCount, productAggregates);



    }
}
