    
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Localization;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Admin.Controllers;

[RemoteService(Name = MarketPlaceRemoteServiceConsts.RemoteServiceName)]
[Area("products")]
[ControllerName("ProductComposition")]
[Route("api/marketplace/product-composition")]
public class ProductCompositionController : AdminController, IProductCompositionService
{
    private readonly IProductAppService _productAppService;
    private readonly IProductPriceAppService _productPriceAppService;
    private readonly IProductStockAppService _productStockAppService;
    private readonly ILocalEventBus _localEventBus;
    private readonly IMediator _mediator;
    public ProductCompositionController(IProductAppService productAppService,
        IProductPriceAppService productPriceAppService,
        ILocalEventBus localEventBus,
        IProductStockAppService productStockAppService,
        IMediator mediator)
    {
        LocalizationResource = typeof(MarketPlaceResource);
        _productAppService = productAppService;
        _productPriceAppService = productPriceAppService;
        _productStockAppService = productStockAppService;
        _localEventBus = localEventBus;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductViewModelCompositionDto> GetAsync(Guid id)
    {
        var productPrice=_productPriceAppService.GetAsync(id);
        var product = _productAppService.GetAsync(id);
        await Task.WhenAll(product,productPrice);

        return new ProductViewModelCompositionDto()
        {
            Product = product.Result,
            ProductPrice = productPrice.Result
        };
    }

    [HttpGet]
    public async Task<ProductListDto> GetProducts(ProductFilterDto input)
    {
        var salesInput = new ProductListFilterDto();
        salesInput.Name = input.Name;
        salesInput.SkipCount = input.SkipCount;
        salesInput.MaxResultCount=input.MaxResultCount;

        var result = new ProductListDto();
        var products=await _productAppService.GetListAsync(salesInput);
        foreach (var product in products.Items)
        {
            var newProductCompositionDto = new ProductViewModelCompositionDto();
            newProductCompositionDto.Product= product;
            result.Products.Add(newProductCompositionDto);
        }
        result.Products= await _mediator.Send(new ProductListRequested()
        {
            Products = result.Products
        });
        //await _localEventBus.PublishAsync(new ProductListRequested()
        //{
        //    Products = result.Products
        //});
        return result;

    }

    [HttpPost]
    public async Task<ProductViewModelCompositionDto> CreateProduct(CreateCompleteProductDto input)
    {
        var newguid = GuidGenerator.Create();
        var setproduct = _productAppService.CreateAsync(newguid, new CreateProductDto
        {
            Name = input.Name,
            Description = input.Description
        });

        var setproductPrice =  _productPriceAppService.CreateAsync(newguid, new CreateProductPriceDto
        {
            Price = input.Price
        });

        var setproductStock = _productStockAppService.CreateAsync(newguid, new CreateProductStockDto
        {
            StockCount = input.StockCount
        });

        await Task.WhenAll(setproduct, setproductPrice, setproductStock);

        return new ProductViewModelCompositionDto()
        {
            Product = setproduct.Result,
            ProductPrice = setproductPrice.Result,
            ProductStock = setproductStock.Result
        };
    }

   // [HttpPost]
   // public async Task<ProductViewModelCompositionDto> CreateProductMediatR(CreateCompleteProductDto input)
   // {
   //     await _mediator.Send(new ProductCreateRequested()
   //     {
   //         Product = input.
   //     }); ;

      

   //;

   //     return new ProductViewModelCompositionDto()
   //     {
   //         Product = setproduct.Result,
   //         ProductPrice = setproductPrice.Result,
   //         ProductStock = setproductStock.Result
   //     };
   // }
}
