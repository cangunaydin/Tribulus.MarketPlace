using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Products.Messages;

namespace Tribulus.MarketPlace.Admin.Marketing.Courier.Activities
{
    public class ProductMarketingSubmittedActivity : IActivity<SubmitMarketingProductArgument, SubmitMarketingProductLog>
    {
        private readonly ILogger<ProductMarketingSubmittedActivity> _logger;
        private readonly IProductAppService _productAppService;

        public ProductMarketingSubmittedActivity(ILogger<ProductMarketingSubmittedActivity> logger, IProductAppService productAppService)
        {
            _logger = logger;
            _productAppService = productAppService;
        }


        public async Task<CompensationResult> Compensate(CompensateContext<SubmitMarketingProductLog> context)
        {
            _logger.LogInformation($"Submit Product Courier compensated called for order {context.Log.ProductId}");
            return context.Compensated();
        }

        public Task<ExecutionResult> Execute(ExecuteContext<SubmitMarketingProductArgument> context)
        {
            //return context.Completed(new { OrderId = order.Id });
            throw new NotImplementedException();
        }
    }
}
