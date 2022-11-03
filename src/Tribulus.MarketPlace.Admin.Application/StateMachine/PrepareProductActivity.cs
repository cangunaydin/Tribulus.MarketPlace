using MassTransit;
using MassTransit.Courier.Contracts;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Components.ItineraryPlanners;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.StateMachine
{
    public class PrepareProductActivity :
        IStateMachineActivity<ProductState>, IConsumer<RoutingSlipFaulted>
    {
        private readonly IRoutingSlipItineraryPlanner<Product> _planner;
        //private readonly BehaviorContext<FutureState, Product> _behaviourProduct;
        public PrepareProductActivity(IRoutingSlipItineraryPlanner<Product> planner)
        {
            _planner = planner;
            //_behaviourProduct = behaviourProduct;
        }


        public void Probe(ProbeContext context)
        {
            context.CreateScope("prepareBurger");
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        //public async Task Execute(BehaviorContext<ProductState> context, IBehavior<ProductState> next)
        //{
        //    await Execute(context);

        //    await next.Execute(context);
        //}

        //public async Task Execute<T>(BehaviorContext<ProductState, T> context, IBehavior<ProductState, T> next) where T : class
        //{
        //    await Execute(context);

        //    await next.Execute(context);
        //}


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


        public Task Execute<T>(BehaviorContext<ProductState, T> context, IBehavior<ProductState, T> next) where T : class
        {
            throw new NotImplementedException();
        }


        public async Task Execute(BehaviorContext<ProductState> context, IBehavior<ProductState> next)
        {

            var trackingNumber = NewId.NextGuid();

            var builder = new RoutingSlipBuilder(trackingNumber);

            builder.AddSubscription(context.ReceiveContext.InputAddress, RoutingSlipEvents.Completed | RoutingSlipEvents.Faulted);

            //if (consumeContext.ExpirationTime.HasValue)
            //    builder.AddVariable("Deadline", consumeContext.ExpirationTime.Value);

            builder.AddVariable("SubmitProductId", context.Saga.Product.ProductId);
            builder.AddVariable("ProductId", context.Saga.CorrelationId);
            try
            {

                await _planner.PlanItinerary(context.Saga.Product, builder);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            var routingSlip = builder.Build();

            await context.Execute(routingSlip).ConfigureAwait(false);
        }

        public Task Consume(ConsumeContext<RoutingSlipFaulted> context)
        {
            throw new NotImplementedException();
        }
    }
}