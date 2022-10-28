using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Inventory.Courier.Activities;
using Tribulus.MarketPlace.Admin.Marketing.Courier.Activities;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Courier.Consumers
{
    public class ProductTransactionConsumer : IConsumer<FullfillProductTransactionMessage>
    {
        private readonly ILogger<ProductTransactionConsumer> _logger;

        public ProductTransactionConsumer(ILogger<ProductTransactionConsumer> logger)
        {
            _logger = logger;            
        }

        public async Task Consume(ConsumeContext<FullfillProductTransactionMessage> context)
        {

            _logger.LogInformation($"Fullfilled Product {context.Message.Name}");
            var builder = new RoutingSlipBuilder(NewId.NextGuid());
            var submitMarketingUrl = QueueNames.GetActivityUri(nameof(ProductMarketingActivity));

            builder.AddActivity("SubmitMarketingProduct", submitMarketingUrl, new
            {
                Name = context.Message.Name,
                Description = context.Message.Description
            });

            builder.AddActivity("SubmitInventoryProduct", QueueNames.GetActivityUri(nameof(ProductInventoryActivity)), new
            {
                context.Message.ProductId,
                context.Message.StockCount                
            });

            //builder.AddActivity("SubmitSalesProduct", QueueNames.GetActivityUri(nameof(ProductSalesActivity)), new
            //{
            //    context.Message.ProductId,
            //    context.Message.Price
            //});
            builder.AddVariable("ProductId", context.Message.ProductId);

            await builder.AddSubscription(context.SourceAddress,
                RoutingSlipEvents.Faulted | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None, x => x.Send<ProductFullfilledFaulted>(new { context.Message.ProductId }));

            await builder.AddSubscription(context.SourceAddress,
                RoutingSlipEvents.Completed | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None, x => x.Send<ProductFullfillCompleted>(new { context.Message.ProductId }));

            var routingSlip = builder.Build();
            await context.Execute(routingSlip); 
            //await context.Execute(routingSlip).ConfigureAwait(true);
            

        }
    }
}