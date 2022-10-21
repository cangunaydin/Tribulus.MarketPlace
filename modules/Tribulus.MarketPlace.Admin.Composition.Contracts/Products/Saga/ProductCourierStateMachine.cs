using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products.Events;

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
            //Initially(
            //   When(ProductSubmitted)
            //   .Then(x => x.Instance.ProductId = x.Data.ProductId)
            //   .Then(x => logger.LogInformation($"Order Transaction {x.Instance.ProductId} submitted"))
            //   .Activity(c => c.OfType<ProductTransactionSubmittedActivity>())
            //   .TransitionTo(Submitted)
            //   );
        }
        private void ConfigureCorrelationIds()
        {
            //    Event(() => OrderSubmitted, x => x.CorrelateById(x => x.Message.CorrelationId)
            //           .SelectId(c => c.Message.CorrelationId));

        }
        public State Submitted { get; private set; }

        public Event<ProductSubmittedEvent> ProductSubmitted { get; private set; }

    }
}
