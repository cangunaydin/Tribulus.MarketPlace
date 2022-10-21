using MassTransit.Courier.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;

namespace Tribulus.MarketPlace.Admin.Marketing.Courier.Consumers
{
    public class FulfilledMarketingProductConsumer : IConsumer<FullfillMarketingProductMessage>
    {
        private readonly ILogger<FulfilledMarketingProductConsumer> _logger;
        private readonly IProductAppService _productAppService;

        public FulfilledMarketingProductConsumer(ILogger<FulfilledMarketingProductConsumer> logger,IProductAppService productAppService)
        {
            _logger = logger;
            _productAppService = productAppService;
        }

        public async Task Consume(ConsumeContext<FullfillMarketingProductMessage> context)
        {
            _logger.LogInformation($"Fullfilled marketing consumer called for product {context}");

        }
    }
}
