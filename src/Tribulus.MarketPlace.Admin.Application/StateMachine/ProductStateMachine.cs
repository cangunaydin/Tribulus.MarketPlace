using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System;
using Tribulus.MarketPlace.Admin.Models;
using Tribulus.MarketPlace.Admin.StateMachine;
using MassTransit.Initializers;

namespace Tribulus.MarketPlace.Admin.Products.StateMachine
{
    public class ProductStateMachine :
        MassTransitStateMachine<ProductState>
    {
        private readonly ILogger<ProductStateMachine> _logger;

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
                        logger.LogInformation("ProductStateMachine RequestId: {RequestId}", context.RequestId);

                        context.Saga.ProductId = context.Message.ProductId;
                        context.Saga.Product = context.Message.Product;
                    })
                    .Activity(x => x.OfInstanceType<PrepareProductActivity>())
                    .RequestStarted()
                    .TransitionTo(WaitingForCompletion)
            );

            // During(Completed,
            //     When(ProductRequested)
            //         .RespondAsync(x => x.CreateBurgerCompleted())
            // );

            // During(Faulted,
            //     When(ProductRequested)
            //         .RespondAsync(x => x.CreateBurgerFaulted())
            // );
            During(WaitingForCompletion,
               When(ProductRequested)
                   .Then(context =>
                   {
                       logger.LogInformation("ProductStateMachine RequestId: {RequestId} (duplicate request)", context.RequestId);
                   })
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
        }

      

        public State WaitingForCompletion { get; }
        public State Completed { get; }
        public State Faulted { get; }

        public Event<RequestProduct> ProductRequested { get; }
        public Event<RoutingSlipCompleted> ProductCompleted { get; }
        public Event<RoutingSlipFaulted> ProductFaulted { get; }

        private Task<ProductCreateFaulted> CreateProductFaulted(BehaviorContext<ProductState, RoutingSlipFaulted> context)
        {
           return context.Init<ProductCreateFaulted>(new
            {
                context.Saga.ProductId,
                context.Saga.Product
            }).Select(x => x.Message);
        }

        private Task<ProductCreateCompleted> CreateProductCompleted(BehaviorContext<ProductState, RoutingSlipCompleted> context)
        {
            return context.Init<ProductCreateCompleted>(new
            {
                context.Saga.ProductId,
                context.Saga.Product
            }).Select(x => x.Message);
        }


        //private Task<object> CreateProductCompleted(BehaviorContext<ProductState, RoutingSlipCompleted> context)
        //{
        //    return context.Init<object>(new
        //    {
        //        context.Message.ProductId,
        //        context.Message.Product
        //    }).Select(x => x.Message);
        //}

        //static Task<ProductCreateCompleted> CreateProductCompleted<T>(BehaviorContext<ProductTransactionState, ProductCreateCompleted> context)
        //     where T : class
        //{
        //    return context.Init<ProductCreateCompleted>(new
        //    {
        //        context.Message.ProductId,
        //        context.Message.Product
        //    }).Select(x=>x.Message);
        //}

        //static Task<ProductCreateFaulted> CreateProductFaulted<T>(BehaviorContext<ProductTransactionState, ProductCreateFaulted> context)
        //    where T : class
        //{
        //    return context.Init<ProductCreateFaulted>(new
        //    {
        //        context.Message.ProductId,
        //        context.Message.Product,
        //        context.Message.Reason
        //    }).Select(x => x.Message);
        //}

    }


    //public static class BurgerStateMachineExtensions
    //{
    //    public static EventActivityBinder<ProductState, RoutingSlipCompleted> CompleteBurger(this EventActivityBinder<ProductState, RoutingSlipCompleted> binder)
    //    {
    //        return binder.Then(context => context.Saga.Product = context.GetVariable<Product>("Product"));
    //    }

    //    public static EventActivityBinder<ProductState, RoutingSlipFaulted> FaultBurger(this EventActivityBinder<ProductState, RoutingSlipFaulted> binder)
    //    {
    //        return binder.Then(context => context.Saga.ExceptionInfo = context.Message.ActivityExceptions.Select(x => x.ExceptionInfo).FirstOrDefault());
    //    }

    //    public static Task<ProductCreateCompleted> CreateBurgerCompleted<T>(this ConsumeEventContext<ProductState, T> context)
    //        where T : class
    //    {
    //        return context.Init<ProductCreateCompleted>(new
    //        {
    //            context.Saga.Created,
    //            context.Saga.Completed,
    //            context.Saga.OrderId,
    //            OrderLineId = context.Saga.CorrelationId,
    //            Description = context.Saga.Product.ToString(),
    //            context.Saga.Product
    //        });
    //    }

    //    public static Task<BurgerFaulted> CreateBurgerFaulted<T>(this ConsumeEventContext<ProductState, T> context)
    //        where T : class
    //    {
    //        return context.Init<BurgerFaulted>(new
    //        {
    //            context.Instance.Created,
    //            context.Instance.Faulted,
    //            context.Instance.OrderId,
    //            OrderLineId = context.Instance.CorrelationId,
    //            Description = context.Instance.Burger.ToString(),
    //            context.Instance.ExceptionInfo
    //        });
    //    }
    //}
}
