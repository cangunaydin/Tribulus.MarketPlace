namespace Tribulus.MarketPlace.Admin.Sales.Products;

//public class GetProductListSubscriber : INotificationHandler<SubscribeGetProductListEto>
//{
//    private readonly IProductPriceAppService _productPriceAppService;
//    public GetProductListSubscriber(IProductPriceAppService productPriceAppService)
//    {
//        _productPriceAppService = productPriceAppService;
//    }

//    public async Task Handle(SubscribeGetProductListEto notification, CancellationToken cancellationToken)
//    {
//        var products = notification.Products;
//        var productPriceInput = new ProductPriceListFilterDto();
//        productPriceInput.Ids = notification.Products.Select(o => o.Id).ToList();
//        var productPrices = await _productPriceAppService.GetListAsync(productPriceInput);
//        foreach (var productPrice in productPrices.Items)
//        {
//            var product = products.FirstOrDefault(o => o.Id == productPrice.Id);
//            if (product != null)
//                product.Price = productPrice.Price;
//        }
//    }

//    //public async Task<List<ProductCompositionDto>> Handle(SubscribeGetProductListEto request, CancellationToken cancellationToken)
//    //{
//    //    var products = request.Products;
//    //    var productPriceInput = new ProductPriceListFilterDto();
//    //    productPriceInput.Ids = request.Products.Select(o => o.Id).ToList();
//    //    var productPrices = await _productPriceAppService.GetListAsync(productPriceInput);
//    //    foreach (var productPrice in productPrices.Items)
//    //    {
//    //        var product = products.FirstOrDefault(o => o.Id == productPrice.Id);
//    //        if (product != null)
//    //            product.Price = productPrice.Price;
//    //    }
//    //    return products;
//    //}

//}
