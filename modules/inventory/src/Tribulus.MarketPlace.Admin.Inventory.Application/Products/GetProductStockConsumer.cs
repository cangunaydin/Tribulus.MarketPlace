using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products.Command;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public class GetProductStockConsumer : IConsumer<GetProductStockRequest>
    {
        private readonly IProductStockAppService _productStockAppService;
        private readonly ILogger<GetProductStockConsumer> _logger;
        private readonly IRepository<ProductStock, Guid> _productStockRepository;
        public GetProductStockConsumer(IProductStockAppService productStockAppService, ILogger<GetProductStockConsumer> logger, IRepository<ProductStock, Guid> productStockRepository)
        {
            _productStockAppService = productStockAppService;
            _productStockRepository = productStockRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetProductStockRequest> context)
        {
            _logger.LogInformation("check context hit");

            var prod = await _productStockRepository.GetAsync(context.Message.ProductId);

            await context.RespondAsync<ProductStockDto>(new
            {
                prod.Id,
                prod.StockCount
            });
        }
    }
}
