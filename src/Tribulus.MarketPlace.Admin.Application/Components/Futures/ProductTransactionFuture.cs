using MassTransit;
using System.Linq;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Components.Futures
{
    public class ProductTransactionFuture : Future<SubmitProduct, SubmitProductCompleted, SubmitProductFaulted>
    {
        public ProductTransactionFuture()
        {
            ConfigureCommand(x => x.CorrelateById(context => context.Message.ProductId));

            SendRequest<Product>(x =>
            {
                x.UsingRequestInitializer(context => new
                {
                    ProductId = context.Saga.CorrelationId,
                    context.Message.Name,
                    context.Message.Description,
                    context.Message.Price,
                    context.Message.StockCount
                });

                x.TrackPendingRequest(message => message.ProductId);
            })
           .OnResponseReceived<ProductCreateCompleted>(x =>
           {
               x.CompletePendingRequest(message => message.ProductId);
           });

            //SendRequests<Product, ProductTransactionProduct>(x => x.Products, x =>
            //{
            //    x.UsingRequestInitializer(MapProductTransactionProduct);
            //    x.TrackPendingRequest(message => message.ProductId);
            //}).OnResponseReceived<ProductCreateCompleted>(x => x.CompletePendingRequest(message => message.ProductId));

            WhenAllCompleted(r => r.SetCompletedUsingInitializer(context =>
            {
                var message = context.GetCommand<SubmitProduct>();

                return new { Description = $"{message.Description} {message.Name} FryShake({context.Saga.Results.Count})" };
            }));

            WhenAnyFaulted(f => f.SetFaultedUsingInitializer(MapProductFaulted));
            //SendRequests<ProductMarketingCreate, ProductMarketing>(x=>x.)
            //   .OnResponseReceived<ProductCompleted>(x => x.CompletePendingRequest(message => message.ProductId));
        }


        static object MapProductFaulted(BehaviorContext<FutureState> context)
        {
            var faults = context.Saga.Faults.ToDictionary(x => x.Key, x => context.ToObject<Fault>(x.Value));

            return new
            {
                LinesFaulted = faults,
                Exceptions = faults.SelectMany(x => x.Value.Exceptions).ToArray()
            };
        }
    }
}
