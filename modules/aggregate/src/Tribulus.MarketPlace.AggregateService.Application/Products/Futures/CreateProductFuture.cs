using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Tribulus.MarketPlace.AggregateService.Products.Events;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.AggregateService.Products.Futures;

public class CreateProductFuture :
       Future<CreateProduct, ProductCreated, ProductCreationFaulted>
{
    public CreateProductFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ProductId));

        ExecuteRoutingSlip(x =>
        {
            x.OnRoutingSlipCompleted(r => r
                .SetCompletedUsingInitializer(context =>
                {
                    var productId = context.GetVariable<Guid>("FutureId");
                    var product = context.GetVariable<ProductDto>("Product");
                    var productPrice = context.GetVariable<ProductPriceDto>("ProductPrice");
                    var productStock = context.GetVariable<ProductStockDto>("ProductStock");

                    return new
                    {
                        Product = new ProductAggregateDto()
                        {
                            Id = productId.GetValueOrDefault(),
                            Name = product.Name,
                            Description = product.Description,
                            Price = productPrice.Price,
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
