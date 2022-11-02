using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Inventory.Components.Activities;
using Tribulus.MarketPlace.Admin.Marketing.Components.Activities;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Components.ItineraryPlanners
{
    public class ProductItineraryPlanner :
    IItineraryPlanner<ProductTransactionProduct>
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

        public async Task PlanItinerary(BehaviorContext<FutureState, ProductTransactionProduct> context, IItineraryBuilder builder)
        {
            _logger.LogInformation($"Product PlanItinerary Executed--> {context.Message.ProductTransactionId}");
            var product = context.Message.Product;

            builder.AddVariable("ProductId", product.ProductId);

            ProductMarketingActivityExtension.AddProductMarketingActivity(builder, _marketingUri, new
            {
                Name = product.Name,
                Description = product.Description
            });

            ProductInventoryActivityExtension.AddProductInventoryActivity(builder, _inventoryUri, new
            {
                context.Message.ProductId,
                product.StockCount
            });

            await Task.WhenAll();
            _logger.LogInformation($"Product PlanItinerary Executed Final--> {context.Message.ProductTransactionId}");
        }
    }
}
