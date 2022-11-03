using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Components.Consumers
{
    public class ProductCreateCompleConsumer { }// :
    //IConsumer<Product>
    //{

    //    public ProductCreateCompleConsumer()
    //    {
    //    }

    //    public async Task Consume(ConsumeContext<Product> context)
    //    {

    //        await context.RespondAsync<ProductCreateCompleted>(new
    //        {
    //            context.Message.ProductId,
    //            context.Message.Name,
    //            context.Message.Price,
    //            context.Message.Description,
    //            context.Message.StockCount
    //        });
    //    }
    //}
}