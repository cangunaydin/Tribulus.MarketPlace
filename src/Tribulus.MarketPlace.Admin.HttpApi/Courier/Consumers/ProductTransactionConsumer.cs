using MassTransit.Courier.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Messages;
using MassTransit.Courier;
using Tribulus.MarketPlace.Admin.Marketing.Courier.Activities;

namespace Tribulus.MarketPlace.Admin.Courier.Consumers
{
    public class ProductTransactionConsumer : IConsumer<FullfillProductTransactionMessage>
    {
        private readonly ILogger<ProductTransactionConsumer> logger;

        public ProductTransactionConsumer(ILogger<FullfillMarketingProductMessage> logger)
        {
            this.logger = (ILogger<ProductTransactionConsumer>)logger;
            //this.dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<FullfillProductTransactionMessage> context)
        {
            //var Baskets = dbContext.OrderItems.Where(x => x.OrderId == context.Message.ProductId)
            //    .Select(p => new { ProductId = p.ProductId, Count = p.Count }).ToList();
            logger.LogInformation($"Fullfilled Product {context.Message.Name}");
            var builder = new RoutingSlipBuilder(NewId.NextGuid());
            var submitMarketingUrl = QueueNames.GetActivityUri(nameof(ProductMarketingSubmittedActivity));
            builder.AddActivity("SubmitMarketingProduct", submitMarketingUrl, new
            {
                Name = context.Message.Name,
                Description = context.Message.Description
            });

            //builder.AddActivity("Payment", QueueNames.GetActivityUri(nameof(PaymentActivity)), new
            //{
            //    context.Message.OrderId,
            //    context.Message.CustomerId,
            //    context.Message.Credit
            //});

            //builder.AddActivity("TakeProduct", QueueNames.GetActivityUri(nameof(TakeProductActivity)), new
            //{
            //    context.Message.OrderId,
            //    Baskets
            //});
            //builder.AddVariable("OrderId", context.Message.OrderId);
            //await builder.AddSubscription(context.SourceAddress,
            //    RoutingSlipEvents.Faulted | RoutingSlipEvents.Supplemental,
            //    RoutingSlipEventContents.None, x => x.Send<OrderFulfillFaulted>(new { context.Message.OrderId }));

            //await builder.AddSubscription(context.SourceAddress,
            //    RoutingSlipEvents.Completed | RoutingSlipEvents.Supplemental,
            //    RoutingSlipEventContents.None, x => x.Send<OrderFullfillCompleted>(new { context.Message.OrderId }));

            //var routingSlip = builder.Build();
            //await context.Execute(routingSlip).ConfigureAwait(false);

        }
    }
}