using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.Admin.Products.Sales;

public class ProductListRequestedEventHandler : INotificationHandler<ProductListRequested>
{
    private readonly IProductPriceAppService _productPriceAppService;


    public ProductListRequestedEventHandler(IProductPriceAppService productPriceAppService)
    {
        _productPriceAppService = productPriceAppService;
    }

    //public async Task<List<ProductViewModelCompositionDto>> Handle(ProductListRequested request, CancellationToken cancellationToken)
    //{
        
    //}

    async Task INotificationHandler<ProductListRequested>.Handle(ProductListRequested notification, CancellationToken cancellationToken)
    {
        var products = notification.Products;
        var productPriceInput = new ProductPriceListFilterDto();
        productPriceInput.Ids = notification.Products.Select(o => o.Product.Id).ToList();
        var productPrices = await _productPriceAppService.GetListAsync(productPriceInput);
        foreach (var productPrice in productPrices.Items)
        {
            var product = products.FirstOrDefault(o => o.Product.Id == productPrice.Id);
            if (product != null)
                product.ProductPrice = productPrice;
        }
    }
}
