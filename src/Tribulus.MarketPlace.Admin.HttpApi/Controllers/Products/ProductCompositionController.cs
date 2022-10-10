using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Localization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Controllers;

[RemoteService(Name = MarketPlaceRemoteServiceConsts.RemoteServiceName)]
[Area("products")]
[ControllerName("ProductComposition")]
[Route("api/marketplace/product-composition")]
public class ProductCompositionController : AdminController, IProductCompositionService
{
    public ProductCompositionController()
    {
        LocalizationResource = typeof(MarketPlaceResource);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductCompositionDto> GetAsync(Guid id)
    {
        var productComposition =  new ProductCompositionDto()
        {
            Id = id
        };
        //var productComposition2=await _requestClientGetProduct.GetResponse<ProductCompositionDto>(productComposition);

        // await _mediator.Publish(new HandleGetProductEto()
        //{
        //     Product = productComposition
        // });
        return productComposition;


        //var productPrice=_productPriceAppService.GetAsync(id);
        //var product = _productAppService.GetAsync(id);
        //await Task.WhenAll(product,productPrice);

        //return new ProductCompositionDto()
        //{
        //    Name = product.Result.Name,
        //    Description = product.Result.Description,
        //    Price = productPrice.Result.Price
        //};
    }

    [HttpGet]
    public async Task<PagedResultDto<ProductCompositionDto>> GetProducts(ProductFilterDto input)
    {
        return null;
       //return await _mediator.Send(new HandleGetProductListEto()
       // {
       //     MaxResultCount = input.MaxResultCount,
       //     Name = input.Name,
       //     SkipCount=input.SkipCount
       // });

        //var salesInput = new ProductListFilterDto();
        //salesInput.Name = input.Name;
        //salesInput.SkipCount = input.SkipCount;
        //salesInput.MaxResultCount=input.MaxResultCount;

        //var result = new ProductListDto();
        //var products=await _productAppService.GetListAsync(salesInput);
        //foreach (var product in products.Items)
        //{
        //    var newProductCompositionDto = new ProductCompositionDto();
        //    newProductCompositionDto.Name= product.Name;
        //    newProductCompositionDto.Description = product.Description;
        //    result.Products.Add(newProductCompositionDto);
        //}
        //result.Products=await _mediator.Send(new ProductListContributeEto()
        //{
        //    Products = result.Products
        //});
        //return result;

    }
}
