
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NUglify.JavaScript;
using System;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Tribulus.MarketPlace.AggregateService.Products.Events;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Products.LocalEvents;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Dapr;

namespace Tribulus.MarketPlace.AggregateService.Products;

public class ProductAggregateAppService : AggregateServiceAppService, IProductAggregateAppService
{
    private readonly IMediator _mediatr;
    private readonly IRequestClient<CreateProduct> _createProductRequestClient;
    private readonly IAbpDaprClientFactory _daprClientFactory;

    public ProductAggregateAppService(
        IMediator mediatr, IRequestClient<CreateProduct> createProductRequestClient, IAbpDaprClientFactory daprClientFactory)
    {
        _mediatr = mediatr;
        _createProductRequestClient = createProductRequestClient;
        _daprClientFactory = daprClientFactory;
        //_daprClientFactory = daprClientFactory;
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
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            var httpContext = HttpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            return await httpContext.GetTokenAsync("access_token");
            Logger.LogInformation("cancellationToken result: " + cancellationToken.ToString());

            //calling auth server for authentication
            //var authServerHttpClient = await _daprClientFactory.InvokeMethodAsync<Order, OrderConfirmation>(
            //"orderservice", "submit", order);
            //var authServerHttpClient = _daprClientFactory.CreateHttpClient();
            //var result = await authServerHttpClient.PostAsync("api/TokenAuth/Authenticate", JsonSerializer.Serialize(new {
            //    Date = DateTime.Parse("2019-08-01"),
            //    TemperatureCelsius = 25,
            //    Summary = "Hot"
            //}));

            // Using Dapr's HttpClient
            var inventoryHttpClient = _daprClientFactory.CreateHttpClient("dapr-inventory-httpapi");
            var result = await inventoryHttpClient.GetStringAsync("api/admin-inventory/product-stock");
           
            Logger.LogInformation("HttpClient result: " + result);
            //var salesHttpClient = _daprClientFactory.CreateHttpClient("dapr-inventory-httpapi");

            // Using Dapr's client
            var daprClient = _daprClientFactory.Create();
            var request = daprClient.CreateInvokeMethodRequest(HttpMethod.Get,"dapr-inventory-httpapi", "api/admin-inventory/product-stock");
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", cancellationToken.ToString());
            var inventoryResult = await daprClient.InvokeMethodWithResponseAsync(request);
            inventoryResult.EnsureSuccessStatusCode();

            //var orderConfirmation = response.Content.ReadFromJsonAsync<OrderConfirmation>();

            //var inventoryResult = await daprClient.InvokeMethodAsync<ProductAggregateDto>(HttpMethod.Get, "dapr-inventory-httpapi", "api/admin-inventory/product-stock", cancellationToken);
            //var salesResult = await daprClient.InvokeMethodAsync<ProductAggregateDto>(HttpMethod.Get, "dapr-sales-httpapi", "api/admin-sales/product-stock/");

            Logger.LogInformation("***DaprClient inventoryResult***: " + inventoryResult);
        }
        catch (Exception ex)
        {
            Logger.LogInformation("***DaprClient inventoryResult***: " + ex);
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
