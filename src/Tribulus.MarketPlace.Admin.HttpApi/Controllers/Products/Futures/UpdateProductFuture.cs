using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using Tribulus.MarketPlace.Admin.Controllers.Products.Commands;
using Tribulus.MarketPlace.Admin.Controllers.Products.Events;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Futures
{
    public class UpdateProductFuture :
       Future<UpdateProduct, ProductUpdated, ProductUpdateFaulted>
    {
        public UpdateProductFuture()
        {
            ConfigureCommand(x => x.CorrelateById(context => context.Message.ProductId));

            ExecuteRoutingSlip(x =>
            {
                x.OnRoutingSlipCompleted(r => r
                    .SetCompletedUsingInitializer(context =>
                    {
                        var productId = context.GetVariable<Guid>("FutureId");
                        //var product = context.Message.GetVariable<ProductDto>("Product");
                        //var productPrice = context.Message.GetVariable<ProductPriceDto>("ProductPrice");
                        var productStock = context.GetVariable<ProductStockDto>("ProductStock");

                        return new
                        {
                            Product = new ProductCompositionDto()
                            {
                                Id = (Guid)productId,
                                //Name = product.Name,
                                //Description = product.Description,
                                //Price = productPrice.Price,
                                StockCount = productStock.StockCount
                            }
                        };
                    }));
                x.OnRoutingSlipFaulted(r => r.SetFaultedUsingInitializer(context =>
                {
                    var productId = context.GetVariable<Guid>("FutureId");

                    IEnumerable<ExceptionInfo> exceptions = context.Message.ActivityExceptions.Select(x => x.ExceptionInfo);

                    var reason = exceptions.FirstOrDefault()?.Message ?? "Unknown";

                    return new
                    {
                        ProductId = productId,
                        Reason = reason
                    };
                }));
            });
        }
    }
}
