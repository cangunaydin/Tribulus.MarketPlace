using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Tribulus.MarketPlace.Admin.Components.Activities;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Products.Saga
{
    public class ProductCourierStateMachine :
        MassTransitStateMachine<ProductTransactionState>
    {
        private readonly ILogger<ProductCourierStateMachine> _logger;

        public ProductCourierStateMachine(ILogger<ProductCourierStateMachine> logger)
        {
            _logger = logger;
            this.InstanceState(x => x.CurrentState);
            this.ConfigureCorrelationIds();
            Initially(
               When(SubmitProduct)
               .Then(x => x.Saga.ProductId = x.Message.ProductTransactionId)
               .Then(x => logger.LogInformation($"Product Transaction {x.Message.ProductTransactionId} submittedb --> State Machine"))
               .Activity(c => c.OfType<ProductTransactionActivity>())
               .TransitionTo(Submitted)
               );           
        }
        private void ConfigureCorrelationIds()
        {
            Event(() => SubmitProduct, x => x.CorrelateById(context => context.Message.ProductTransactionId));

        }

        public State Submitted { get; private set; }

        public Event<SubmitProductTransaction> SubmitProduct { get; private set; }

        void ProductCreated(BehaviorContext<ProductTransactionState, SubmitProductTransactionCompleted> context)
        {
            _logger.LogInformation("State Machine-->Product Created: {0}", context.Message.ProductId);
        }

        void ProductFaulted(BehaviorContext<ProductTransactionState, SubmitProductTransactionFaulted> context)
        {
            _logger.LogInformation("State Machine-->Product Created: {0}", context.Message.ProductId);
        }

    }
}
