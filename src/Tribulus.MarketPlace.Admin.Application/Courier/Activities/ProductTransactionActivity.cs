using Automatonymous;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Events;

namespace Tribulus.MarketPlace.Admin.Courier.Activities
{
    public class ProductTransactionActivity : IStateMachineActivity<ProductTransactionState, SubmitProductEvent>
    {
        private readonly ILogger<ProductTransactionActivity> _logger;

        public ProductTransactionActivity(ILogger<ProductTransactionActivity> logger)
        {
            _logger = logger;
            _logger.LogInformation("ProductTransactionSubmittedActivity called");
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<ProductTransactionState, SubmitProductEvent> context, IBehavior<ProductTransactionState, SubmitProductEvent> next)
        {
            var sendEndpoint = await context.GetSendEndpoint(QueueNames.GetMessageUri(nameof(FullfillProductTransactionMessage)));
            //var sendEndpoint = await context.GetSendEndpoint(new Uri("loopback://localhost/product-transaction-state"));
            _logger.LogInformation($"Call Product Transaction consumer for sendEndpoint {sendEndpoint}");
            await context.Publish<FullfillProductTransactionMessage>(new
            {
                Name = context.Message.Name,
                Description = context.Message.Description,
                Price = context.Message.Price,
                StockCount = context.Message.StockCount
            });

            // call the next activity in the behavior
            //await next.Execute(context).ConfigureAwait(false);
            //throw new NotImplementedException();
        }

        public Task Faulted<TException>(BehaviorExceptionContext<ProductTransactionState, SubmitProductEvent, TException> context, IBehavior<ProductTransactionState, SubmitProductEvent> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("submit-order");
        }
    }
}
