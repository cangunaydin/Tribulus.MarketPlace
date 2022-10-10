using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.Admin.Products.Sales;

public class ProductListRequestedEventHandler : IRequestHandler<ProductListContributeEto, List<ProductCompositionDto>>
{
    private readonly IProductPriceAppService _productPriceAppService;
    public ProductListRequestedEventHandler(IProductPriceAppService productPriceAppService)
    {
        _productPriceAppService = productPriceAppService;
    }

    public async Task<List<ProductCompositionDto>> Handle(ProductListContributeEto request, CancellationToken cancellationToken)
    {
        var products = request.Products;
        var productPriceInput = new ProductPriceListFilterDto();
        productPriceInput.Ids = request.Products.Select(o => o.Id).ToList();
        var productPrices = await _productPriceAppService.GetListAsync(productPriceInput);
        foreach (var productPrice in productPrices.Items)
        {
            var product = products.FirstOrDefault(o => o.Id == productPrice.Id);
            if (product != null)
                product.Price = productPrice.Price;
        }
        return products;
    }

}
