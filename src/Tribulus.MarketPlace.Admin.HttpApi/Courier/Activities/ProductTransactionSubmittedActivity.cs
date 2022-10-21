using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Events;

namespace Tribulus.MarketPlace.Admin.Courier.Activities
{
    public class ProductTransactionSubmittedActivity : IStateMachineActivity<ProductTransactionState, ProductSubmittedEvent>
    {
        private readonly ILogger<ProductTransactionSubmittedActivity> _logger;

        public ProductTransactionSubmittedActivity(ILogger<ProductTransactionSubmittedActivity> logger)
        {
            _logger = logger;
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<ProductTransactionState, ProductSubmittedEvent> context, IBehavior<ProductTransactionState, ProductSubmittedEvent> next)
        {
            var sendEndpoint = await context.GetSendEndpoint(QueueNames.GetMessageUri(nameof(FullfillMarketingProductMessage)));
            _logger.LogInformation($"Marketing Product Transaction activity for sendEndpoint {sendEndpoint} will be called");
            await sendEndpoint.Send<FullfillMarketingProductMessage>(new
            {
                Name = context.Data.Name,
                Price = context.Data.Price,
                StockCount = context.Data.StockCount
            });
            throw new NotImplementedException();
        }

        public Task Faulted<TException>(BehaviorExceptionContext<ProductTransactionState, ProductSubmittedEvent, TException> context, IBehavior<ProductTransactionState, ProductSubmittedEvent> next) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("submit-order");
        }
    }
}
