using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products.Command;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class GetProductConsumer : IConsumer<GetProductRequest>
    {
        private readonly ILogger<GetProductConsumer> _logger;
        private readonly IRepository<Product, Guid> _productRepository;
        public GetProductConsumer(IProductAppService productStockAppService, ILogger<GetProductConsumer> logger, IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetProductRequest> context)
        {
            _logger.LogInformation("check context hit");

            var prod = await _productRepository.GetAsync(context.Message.ProductId);

            await context.RespondAsync<ProductDto>(new
            {
                prod.Id,
                prod.Name,
                prod.Description
            });
        }
    }
}
