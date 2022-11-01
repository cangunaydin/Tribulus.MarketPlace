using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Marketing.Products;
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
            _logger.LogInformation("Marketing Module--> ProductMarketingActivity module called");
            _productAppService = productAppService;
        }


        public async Task<CompensationResult> Compensate(CompensateContext<MarketingProductLog> context)
        {
            _logger.LogInformation($"Marketing Module--> Compensate Product Marketing called for product {context.Log.ProductId} Compensated");
            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<MarketingProductArgument> context)
        {
            var product = Guid.NewGuid();//created product id assign later on after calling insert product service
            context.Arguments.ProductId = product;
            _logger.LogInformation($"Marketing Module--> Execute Marketing Courier called for product {context.Arguments.ProductId} Executed");
            return context.Completed(new { ProductId = product });
        }
    }
}
