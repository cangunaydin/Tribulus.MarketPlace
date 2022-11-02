using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Messages;
using Tribulus.MarketPlace.Admin.Products.Messages;

namespace Tribulus.MarketPlace.Admin.Marketing.Courier.Activities
{
    public class ProductMarketingActivity : IActivity<MarketingProductArgument, MarketingProductLog>
    {
        private readonly ILogger<ProductMarketingActivity> _logger;
        private readonly IProductAppService _productAppService;

        public ProductMarketingActivity(ILogger<ProductMarketingActivity> logger, IProductAppService productAppService)
        {
            _logger = logger;
            _logger.LogInformation("Inventory Module--> ProductInventoryActivity module called");
            _productAppService = productAppService;
        }


        public async Task<CompensationResult> Compensate(CompensateContext<MarketingProductLog> context)
        {
            _logger.LogInformation($"Inventory Module--> Compensate Inventory Courier called for product {context.Log.ProductId} Compensated");
            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<MarketingProductArgument> context)
        {
            _logger.LogInformation($"Inventory Module--> Execute Inventory Courier called for product {context.Arguments.ProductId} Executed");
            //return context.Completed(new { ProductId = Guid.NewGuid() });
            throw new NotImplementedException(); //to check whether it calls the marketing compensate method or not
        }
    }
}
