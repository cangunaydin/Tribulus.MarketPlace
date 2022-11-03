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
               .Then(x => x.Saga.ProductId = x.Message.ProductId)
               .Then(x => logger.LogInformation($"Product Transaction {x.Message.ProductId} submittedb --> State Machine"))
               .Activity(c => c.OfType<ProductTransactionActivity>())
               .TransitionTo(Submitted)
               );           
        }
        private void ConfigureCorrelationIds()
        {
            Event(() => SubmitProduct, x => x.CorrelateById(context => context.Message.ProductId));

        }

        public State Submitted { get; private set; }

        public Event<SubmitProduct> SubmitProduct { get; private set; }

        void ProductCreated(BehaviorContext<ProductTransactionState, SubmitProductCompleted> context)
        {
            _logger.LogInformation("State Machine-->Product Created: {0}", context.Message.ProductId);
        }

        void ProductFaulted(BehaviorContext<ProductTransactionState, SubmitProductFaulted> context)
        {
            _logger.LogInformation("State Machine-->Product Created: {0}", context.Message.ProductId);
        }

    }
}
