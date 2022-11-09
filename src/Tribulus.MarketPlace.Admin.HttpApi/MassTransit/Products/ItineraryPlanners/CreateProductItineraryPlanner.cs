using MassTransit.Courier;
using System;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Extensions;
using Tribulus.MarketPlace.Products.MassTransit.Commands;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Admin.MassTransit.ItineraryPlanners
{
    public class CreateProductItineraryPlanner : IItineraryPlanner<CreateProduct>,ITransientDependency
    {
        private readonly MarketPlaceEndpointNameFormatter _endpointNameformatter;
        public CreateProductItineraryPlanner(MarketPlaceEndpointNameFormatter endpointNameformatter)
        {
            _endpointNameformatter = endpointNameformatter;
        }
        public void AddActivity(CreateProduct value, ItineraryBuilder builder, Type activity)
        {
            var address = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(activity)}");
            builder.AddActivity(activity.Name, address, value);
        }

        public void ProduceItinerary(CreateProduct value, ItineraryBuilder builder)
        {
            var createProductAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductActivity))}");

            builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductActivity)), createProductAddress, new
            {
               ProductId=value.ProductId,
               Name=value.Name,
               Description=value.Description
            });

            var createProductPriceAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductPriceActivity))}");

            builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductPriceActivity)), createProductPriceAddress, new
            {
                ProductId = value.ProductId,
                Price=value.Price
            });

            var createProductStockAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductStockActivity))}");

            builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductStockActivity)), createProductStockAddress, new
            {
                ProductId = value.ProductId,
                StockCount = value.StockCount
            });
        }
    }
}
