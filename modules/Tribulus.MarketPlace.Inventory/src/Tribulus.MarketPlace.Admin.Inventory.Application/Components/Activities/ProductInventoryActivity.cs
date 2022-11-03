using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Messages;

namespace Tribulus.MarketPlace.Admin.Inventory.Components.Activities
{
    public class ProductInventoryActivity : IActivity<InventoryProductArgument, InventoryProductLog>
    {
        private readonly ILogger<ProductInventoryActivity> _logger;
        private readonly IProductStockAppService _productStockAppService;

        public ProductInventoryActivity(ILogger<ProductInventoryActivity> logger, IProductStockAppService productStockAppService)
        {
            _logger = logger;
            _logger.LogInformation("Inventory Module--> ProductInventoryActivity module called");
            _productStockAppService = productStockAppService;
        }


        public async Task<CompensationResult> Compensate(CompensateContext<InventoryProductLog> context)
        {
            _logger.LogInformation($"Inventory Module--> Compensate Inventory Courier called for product {context.Log.ProductId} Compensated");
            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<InventoryProductArgument> context)
        {
            _logger.LogInformation($"Inventory Module--> Execute Inventory Courier called for product {context.Arguments.ProductId} Executed");
            if (context.Arguments != null && context.Arguments.StockCount == 0) {
                _logger.LogInformation($"Low stock -> 0");
                throw new Exception("Low stock -> 0");//to check whether it calls the marketing compensate method or not
            }
            return context.Completed(new { ProductId = context.Arguments.ProductId });
        }
    }
}
