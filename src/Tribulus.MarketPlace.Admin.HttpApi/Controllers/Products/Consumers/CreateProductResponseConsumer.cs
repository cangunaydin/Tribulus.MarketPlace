using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;
using MassTransit.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Controllers.Products.Commands;
using Tribulus.MarketPlace.Admin.Controllers.Products.Events;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Products;
using Tribulus.MarketPlace.RoutingSlip;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Consumers;

public class CreateProductResponseConsumer : RoutingSlipResponseConsumer<CreateProduct, ProductCreated, ProductCreationFaulted>
{
    protected override Task<ProductCreationFaulted> CreateFaultedResponseMessage(ConsumeContext<RoutingSlipFaulted> context, CreateProduct request, Guid requestId)
    {
        var productId = context.Message.GetVariable<Guid>("ProductId");

        IEnumerable<ExceptionInfo> exceptions = context.Message.ActivityExceptions.Select(x => x.ExceptionInfo);

        var reason = exceptions.FirstOrDefault()?.Message ?? "Unknown";

        return MessageInitializerCache<ProductCreationFaulted>.InitializeMessage(context, new
        {
            ProductId=productId,
            Reason=reason
        });
    }

    protected override Task<ProductCreated> CreateResponseMessage(ConsumeContext<RoutingSlipCompleted> context, CreateProduct request)
    {
        var productId = context.Message.GetVariable<Guid>("ProductId");

        HasVariable(context.Message.Variables, "Product", out ProductDto product);
        HasVariable(context.Message.Variables, "ProductPrice", out ProductPriceDto productPrice);
        HasVariable(context.Message.Variables, "ProductStock", out ProductStockDto productStock);



        return MessageInitializerCache<ProductCreated>.InitializeMessage(context, new
        {
            Product = new ProductCompositionDto()
            {
                Id=productId,
                Name=product.Name,
                Description=product.Description,
                Price=productPrice.Price,
                StockCount=productStock.StockCount
            }
        });
    }
}
