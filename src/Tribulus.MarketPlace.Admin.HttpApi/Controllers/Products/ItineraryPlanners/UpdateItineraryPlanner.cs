using MassTransit.Courier;
using MassTransit.Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Controllers.Products.Commands;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Extensions;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.ItineraryPlanners
{
    public class UpdateeProductItineraryPlanner : IItineraryPlanner<UpdateProduct>, ITransientDependency
    {
        private readonly MarketPlaceEndpointNameFormatter _endpointNameformatter;
        public UpdateeProductItineraryPlanner(MarketPlaceEndpointNameFormatter endpointNameformatter)
        {
            _endpointNameformatter = endpointNameformatter;
        }
        public void AddActivity(CreateProduct value, ItineraryBuilder builder, Type activity)
        {
            var address = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(activity)}");
            builder.AddActivity(activity.Name, address, value);
        }

        public Task PlanItinerary(FutureConsumeContext<UpdateProduct> context, ItineraryBuilder builder)
        {
            var product = context.Message;
            var updateProductAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(IUpdateProductStockActivity))}");

            builder.AddVariable(nameof(product.ProductId), product.ProductId);
            builder.AddVariable(nameof(product.UserId), product.UserId);
            builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(IUpdateProductStockActivity)), updateProductAddress, new
            {
                StockCount = product.StockCount
            });

            //var createProductPriceAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductPriceActivity))}");

            //builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductPriceActivity)), createProductPriceAddress, new
            //{
            //    Price = product.Price
            //});

            //var createProductStockAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductStockActivity))}");

            //builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductStockActivity)), createProductStockAddress, new
            //{
            //    StockCount = product.StockCount
            //});
            return Task.CompletedTask;
        }

    }
}
