using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.ViewModelComposition.Contracts.Sales;
using Tribulus.ServiceComposer;

namespace Tribulus.MarketPlace.Admin.ViewModelComposition.Sales
{
    public class ProductSubscriptionService : CompositionService, IProductSubscriptionService, ICompositionSubscribeService
    {
        public void GetProducts(ProductFilterDto input)
        {
            CompositionContext.Subscribe<ProductListRequested>(o =>
            {
                var viewModel = CompositionContext.HttpRequest.GetComposedResponseModel<ProductListDto>();
                //foreach (var productId in o.ProductIds)
                //{
                //    var productViewModelItem = viewModel.Products.First(o => o.Product.Id == productId);
                //    var random = new Random();
                //    productViewModelItem.ProductPrice = new Admin.Sales.ProductPriceDto()
                //    {
                //        Id = productId,
                //        Price = random.Next() * 500
                //    };
                //}
                return Task.CompletedTask;
            });
        }
    }
}
