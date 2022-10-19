    
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Events;
using Tribulus.MarketPlace.Localization;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp;
using Volo.Abp.EventBus.Local;

namespace Tribulus.MarketPlace.Admin.Controllers;

[ControllerName("ProductComposition")]
[Route("api/composition/product")]
public class ProductCompositionController : AdminController, IProductCompositionService
{
    private readonly IProductAppService _productAppService;
    private readonly IProductPriceAppService _productPriceAppService;
    private readonly ILocalEventBus _localEventBus;
    private readonly IMediator _mediator;
    public ProductCompositionController(IProductAppService productAppService,
        IProductPriceAppService productPriceAppService,
        ILocalEventBus localEventBus,
        IMediator mediator)
    {
        LocalizationResource = typeof(MarketPlaceResource);
        _productAppService = productAppService;
        _productPriceAppService = productPriceAppService;
        _localEventBus = localEventBus;
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
        result.Products=await _mediator.Send(new ProductListRequested()
        {
            Filter = input,
            Products = null
        };
        await _mediator.Publish(productListEto);
        return productListEto.Products;

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
