using MassTransit;
using MassTransit.Courier.Contracts;
using MassTransit.Initializers;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Components.Activities;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Products.StateMachine
{
    public class ProductStateMachine :
        MassTransitStateMachine<ProductState>
    {
        private readonly ILogger<ProductStateMachine> _logger;
        public State WaitingForCompletion { get; }
        public State Completed { get; }
        public State Faulted { get; }

        public Event<RequestProduct> ProductRequested { get; }
        public Event<RoutingSlipCompleted> ProductCompleted { get; }
        public Event<RoutingSlipFaulted> ProductFaulted { get; }


        public ProductStateMachine(ILogger<ProductStateMachine> logger)
        {
            _logger = logger;

            Event(() => ProductRequested, x => x.CorrelateById(context => context.Message.Product.ProductId));
            Event(() => ProductCompleted, x => x.CorrelateById(instance => instance.TrackingNumber, context => context.Message.TrackingNumber));
            Event(() => ProductFaulted, x => x.CorrelateById(instance => instance.TrackingNumber, context => context.Message.TrackingNumber));

            InstanceState(x => x.CurrentState, WaitingForCompletion, Completed, Faulted);


            Initially(
                When(ProductRequested)
                    .Then(context =>
                    {
                        _logger.LogInformation("ProductStateMachine RequestId: {RequestId}", context);
                        context.Saga.ProductId = context.Message.ProductId;
                        context.Saga.Product = context.Message.Product;
                    })
                    .Activity(x => x.OfInstanceType<PrepareProductActivity>())
                    .RequestStarted()
                    .TransitionTo(WaitingForCompletion)
            );


            During(WaitingForCompletion,
               When(ProductRequested)
                   .RequestStarted(),
                When(ProductCompleted)
                    .Then(context => context.Saga.Product = context.GetVariable<Product>("Product"))
                    .RequestCompleted(x => CreateProductCompleted(x))
                    .TransitionTo(Completed),
                When(ProductFaulted)
                    .Then(context =>
                    {
                        context.Saga.Reason = context.Message.ActivityExceptions.Select(x => x.ExceptionInfo).FirstOrDefault()?.Message ?? "Unknown";
                    })
                    .RequestCompleted(x => CreateProductFaulted(x))
                    .TransitionTo(Faulted));


            During(Completed,
                When(ProductRequested)
                    .RespondAsync(x => CreateProductCompleted((BehaviorContext<ProductState, RoutingSlipCompleted>)x))
            );

            During(Faulted,
                When(ProductRequested)
                    .RespondAsync(x => CreateProductFaulted((BehaviorContext<ProductState, RoutingSlipFaulted>)x))
            );
        }


        static Task<ProductCreateFaulted> CreateProductFaulted(BehaviorContext<ProductState, RoutingSlipFaulted> context)
        {
            return context.Init<ProductCreateFaulted>(new
            {
                context.Saga.ProductId,
                context.Saga.Product,
                context.Saga.Reason
            }).Select(x => x.Message);
        }

        static Task<ProductCreateCompleted> CreateProductCompleted(BehaviorContext<ProductState, RoutingSlipCompleted> context)
        {
            return context.Init<ProductCreateCompleted>(new
            {
                context.Saga.ProductId,
                context.Saga.Product
            }).Select(x => x.Message);
        }
    }
}
