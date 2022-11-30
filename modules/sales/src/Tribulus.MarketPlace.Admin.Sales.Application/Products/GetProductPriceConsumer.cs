using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Sales.Products.Command;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public class GetProductPriceConsumer : IConsumer<GetProductPriceRequest>
    {
        private readonly ILogger<GetProductPriceConsumer> _logger;
        private readonly IRepository<ProductPrice, Guid> _productPriceRepository;
        public GetProductPriceConsumer( ILogger<GetProductPriceConsumer> logger, IRepository<ProductPrice, Guid> productPriceRepository)
        {
            _productPriceRepository = productPriceRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetProductPriceRequest> context)
        {
            _logger.LogInformation("check context hit");

            var prod = await _productPriceRepository.GetAsync(context.Message.ProductId);

            await context.RespondAsync<ProductPriceDto>(new
            {
                prod.Id,
                prod.Price
            });
        }
    }
}
