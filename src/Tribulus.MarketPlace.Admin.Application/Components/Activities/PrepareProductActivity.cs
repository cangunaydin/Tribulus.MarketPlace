using MassTransit;
using MassTransit.Courier.Contracts;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Components.ItineraryPlanners;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Components.Activities
{
    public class PrepareProductActivity :
        IStateMachineActivity<ProductState>
    {
        private readonly IRoutingSlipItineraryPlanner<Product> _planner;

        public PrepareProductActivity(IRoutingSlipItineraryPlanner<Product> planner)
        {
            _planner = planner;
        }


        public void Probe(ProbeContext context)
        {
            context.CreateScope("prepare-product");
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<ProductState, TException> context, IBehavior<ProductState> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public Task Faulted<T, TException>(BehaviorExceptionContext<ProductState, T, TException> context, IBehavior<ProductState, T> next)
        where T : class
        where TException : Exception
        {
            return next.Faulted(context);
        }


        public async Task Execute<T>(BehaviorContext<ProductState, T> context, IBehavior<ProductState, T> next) where T : class
        {
            await Execute(context);
            await next.Execute(context);
        }

        public async Task Execute(BehaviorContext<ProductState> context, IBehavior<ProductState> next)
        {
            await Execute(context);
            await next.Execute(context);
        }

        async Task Execute(BehaviorContext<ProductState> context)
        {
            var trackingNumber = NewId.NextGuid();

            var builder = new RoutingSlipBuilder(trackingNumber);

            builder.AddSubscription(context.ReceiveContext.InputAddress, RoutingSlipEvents.Completed | RoutingSlipEvents.Faulted);

            //if (consumeContext.ExpirationTime.HasValue)
            //    builder.AddVariable("Deadline", consumeContext.ExpirationTime.Value);

            builder.AddVariable("RequestId", context.RequestId);
            builder.AddVariable("SubmitProductId", context.Saga.Product.ProductId);
            builder.AddVariable("ProductId", context.Saga.CorrelationId);

            await _planner.PlanItinerary(context.Saga.Product, builder);

            var routingSlip = builder.Build();

            await context.Execute(routingSlip).ConfigureAwait(false);
            context.Saga.TrackingNumber = trackingNumber;

        }
    }
}