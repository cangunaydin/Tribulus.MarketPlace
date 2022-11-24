using MassTransit;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.AggregateService.Constants;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.AggregateService.Products.ItineraryPlanners
{
    public class CreateProductItineraryPlanner : IItineraryPlanner<CreateProduct>, ITransientDependency
    {
        private readonly Uri _marketingUri;
        private readonly Uri _inventoryUri;
        private readonly Uri _salesUri;

        public CreateProductItineraryPlanner()
        {
            _marketingUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductMarketingActivityUri);
            _inventoryUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductInventoryActivityUri);
            _salesUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductSalesActivityUri);

        }
        //public void AddActivity(CreateProduct value, IItineraryBuilder builder, Type activity)
        //{
        //    var address = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(activity)}");
        //    builder.AddActivity(activity.Name, address, value);
        //}
        public Task PlanItinerary(BehaviorContext<FutureState, CreateProduct> value, IItineraryBuilder builder)
        {
            var product = value.Message;
            var createProductAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductActivity))}");

            builder.AddVariable(nameof(product.ProductId), product.ProductId);
            builder.AddVariable(nameof(product.UserId), product.UserId);
            //builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductActivity)), createProductAddress, new
            //{
            //    Name = product.Name,
            //    Description = product.Description
            //});

            //var createProductPriceAddress = new Uri($"exchange:{_endpointNameformatter.ExecuteActivity(typeof(ICreateProductPriceActivity))}");

            //builder.AddActivity(_endpointNameformatter.GetActivityName(typeof(ICreateProductPriceActivity)), createProductPriceAddress, new
            //{
            //    Price = product.Price
            //});
            builder.AddActivity(EndpointsUri.ProductMarketingActivityUri, _marketingUri, new
            {
                product.Name,
                product.Description
            });

            builder.AddActivity(EndpointsUri.ProductInventoryActivityUri, _inventoryUri, new
            {
                product.StockCount
            });

            builder.AddActivity(EndpointsUri.ProductSalesActivityUri, _salesUri, new
            {
                product.Price
            });
            return Task.CompletedTask;
        }

        
    }
}
