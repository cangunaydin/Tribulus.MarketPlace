using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Inventory.Components.Activities;
using Tribulus.MarketPlace.Admin.Marketing.Components.Activities;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Components.ItineraryPlanners
{
    public class ProductItineraryPlanner: IRoutingSlipItineraryPlanner<Product>
    {
        private readonly ILogger<ProductItineraryPlanner> _logger;

        private readonly Uri _marketingUri;
        private readonly Uri _inventoryUri;

        public ProductItineraryPlanner(ILogger<ProductItineraryPlanner> logger, IEndpointNameFormatter formatter)
        {
            _logger = logger;
            _marketingUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductMarketingActivityUri);
            _inventoryUri = new Uri(EndpointsUri.MainUri + EndpointsUri.ProductInventoryActivityUri);
        }

        public Task PlanItinerary(Product product, IItineraryBuilder builder)
        {
            _logger.LogInformation($"Product PlanItinerary Executed--> {product.ProductId}");            

            builder.AddVariable("ProductId", product.ProductId);

            ProductMarketingActivityExtension.AddProductMarketingActivity(builder, _marketingUri, new
            {
                product.Name,
                product.Description
            });

            ProductInventoryActivityExtension.AddProductInventoryActivity(builder, _inventoryUri, new
            {
                product.ProductId,
                product.StockCount
            });

            _logger.LogInformation($"Product PlanItinerary Executed Completed--> {product.ProductId}");
            return Task.CompletedTask;
        }
    }
}
