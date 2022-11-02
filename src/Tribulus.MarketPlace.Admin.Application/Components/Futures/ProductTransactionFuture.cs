using MassTransit;
using System.Linq;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Futures
{
    public class ProductTransactionFuture : Future<SubmitProductTransaction, SubmitProductTransactionCompleted, SubmitProductTransactionFaulted>
    {
        public ProductTransactionFuture()
        {
            ConfigureCommand(x => x.CorrelateById(context => context.Message.ProductTransactionId));

            SendRequests<Product, ProductTransaction>(x => x.Products, x =>
            {
                x.UsingRequestInitializer(MapProductCreate);
                x.TrackPendingRequest(message => message.ProductId);
            }).OnResponseReceived<ProductCreateCompleted>(x => x.CompletePendingRequest(message => message.ProductId));

            WhenAllCompleted(r => r.SetCompletedUsingInitializer(context => new
            {
                LinesCompleted = context.Saga.Results.Select(x => context.ToObject<SubmitProductTransactionCompleted>(x.Value)).ToDictionary(x => x.ProductId),
            }));

            WhenAnyFaulted(f => f.SetFaultedUsingInitializer(MapProductFaulted));
            //SendRequests<ProductMarketingCreate, ProductMarketing>(x=>x.)
            //   .OnResponseReceived<ProductCompleted>(x => x.CompletePendingRequest(message => message.ProductId));
        }


        static object MapProductCreate(BehaviorContext<FutureState, Product> context)
        {
            return new
            {
                ProductId = context.Saga.CorrelationId,
                Name = context.Message.Name,
                Description = context.Message.Description,
                Price = context.Message.Price,
                StockCount = context.Message.StockCount,
            };
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
