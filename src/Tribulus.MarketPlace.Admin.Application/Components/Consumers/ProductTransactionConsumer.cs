using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Inventory.Components.Activities;
using Tribulus.MarketPlace.Admin.Marketing.Components.Activities;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Components.Consumers
{
    public class ProductTransactionConsumer : IConsumer<FullfillProductTransactionMessage>
    {
        private readonly ILogger<ProductTransactionConsumer> _logger;
        private readonly Uri _marketingUri;
        private readonly Uri _inventoryUri;

        public ProductTransactionConsumer(ILogger<ProductTransactionConsumer> logger)
        {
            _logger = logger;
            _marketingUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductMarketingActivityUri);
            _inventoryUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductInventoryActivityUri);
        }

        public async Task Consume(ConsumeContext<FullfillProductTransactionMessage> context)
        {

            _logger.LogInformation($"Product Transaction Consumer Executed--> {context.Message.Name}");
            RoutingSlipBuilder builder = new RoutingSlipBuilder(NewId.NextGuid());
            ProductMarketingActivityExtension.AddProductMarketingActivity(builder, _marketingUri, new
            {
                Name = context.Message.Name,
                Description = context.Message.Description
            });

            ProductInventoryActivityExtension.AddProductInventoryActivity(builder, _inventoryUri, new
            {
                context.Message.ProductId,
                context.Message.StockCount
            });

            //builder.AddActivity("product-marketing-activity", QueueNames.GetActivityUri(nameof(ProductMarketingActivity)), new
            //{
            //    Name = context.Message.Name,
            //    Description = context.Message.Description
            //});

            //builder.AddActivity("product-inventory-activity", QueueNames.GetActivityUri(nameof(ProductInventoryActivity)), new
            //{
            //    context.Message.ProductId,
            //    context.Message.StockCount
            //});

            //builder.AddActivity("SubmitSalesProduct", QueueNames.GetActivityUri(nameof(ProductSalesActivity)), new
            //{
            //    context.Message.ProductId,
            //    context.Message.Price
            //});
            builder.AddVariable("ProductId", context.Message.ProductId);

            await builder.AddSubscription(context.SourceAddress,
                RoutingSlipEvents.Faulted | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None, x => x.Send<SubmitProductFaulted>(new { context.Message.ProductId }));

            await builder.AddSubscription(context.SourceAddress,
                RoutingSlipEvents.Completed | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None, x => x.Send<SubmitProductCompleted>(new { context.Message.ProductId }));

            var routingSlip = builder.Build();
            await context.Execute(routingSlip);
            //await context.Execute(routingSlip).ConfigureAwait(true);


        }

        public Task Consume(ConsumeContext<RoutingSlipCompleted> context)
        {
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<RoutingSlipFaulted> context)
        {
            throw new NotImplementedException();
        }
    }
}