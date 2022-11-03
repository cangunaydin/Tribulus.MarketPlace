using MassTransit;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Components.Futures
{
    public class ProductFuture : Future<Product, ProductCreateCompleted>
    {
        public ProductFuture()
        {
            ConfigureCommand(x => x.CorrelateById(context => context.Message.ProductId));

            //ExecuteRoutingSlip(x => x
            //    .OnRoutingSlipCompleted(r => r
            //        .SetCompletedUsingInitializer(context =>
            //        {
            //            var product = context.GetVariable<Product>(nameof(ProductCreateCompleted));

            //            return new
            //            {
            //                Name = product.Name,
            //                Description = product.Description,
            //                Price = product.Price,
            //                StockCount = product.StockCount
            //            };
            //        })));
        }
    }
}
