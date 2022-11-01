using MassTransit;
using System;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Futures
{
    public class ProductFuture : Future<SubmitProduct, ProductCompleted, ProductFaulted>
    {
        public ProductFuture()
        {
            ConfigureCommand(x => x.CorrelateById(context => context.Message.ProductId));


            //SendRequests<ProductMarketingCreate, ProductMarketing>(x=>x.)
            //   .OnResponseReceived<ProductCompleted>(x => x.CompletePendingRequest(message => message.ProductId));
        }
    }
}
