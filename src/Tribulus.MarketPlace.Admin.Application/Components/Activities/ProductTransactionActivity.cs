using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Components.Activities
{
    public class ProductTransactionActivity : IStateMachineActivity<ProductTransactionState, SubmitProductTransaction>
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

        public async Task Execute(BehaviorContext<ProductTransactionState, SubmitProductTransaction> context, IBehavior<ProductTransactionState, SubmitProductTransaction> next)
        {
            _logger.LogInformation("Call Product Transaction consumer for FullfillProductTransactionMessage");
            await context.Publish<FullfillProductTransactionMessage>(new
            {
                //Name = context.Message.Name,
                //Description = context.Message.Description,
                //Price = context.Message.Price,
                //StockCount = context.Message.StockCount
            });

            // call the next activity in the behavior
            //await next.Execute(context).ConfigureAwait(false);
            //throw new NotImplementedException();
        }

        public Task Faulted<TException>(BehaviorExceptionContext<ProductTransactionState, SubmitProductTransaction, TException> context, IBehavior<ProductTransactionState, SubmitProductTransaction> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("submit-order");
        }
    }
}
