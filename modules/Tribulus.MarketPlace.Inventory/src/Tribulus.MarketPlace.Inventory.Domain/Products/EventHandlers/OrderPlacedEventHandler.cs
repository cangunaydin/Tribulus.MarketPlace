using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Orders;
using Tribulus.MarketPlace.Inventory.Products.Events;
using Tribulus.MarketPlace.Sales.Orders.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.Inventory.Products.EventHandlers
{
    public class OrderPlacedEventHandler : IDistributedEventHandler<OrderPlacedEto>,
          ITransientDependency
    {
        private readonly IRepository<ProductStock, Guid> _productStockRepository;
        private readonly IRepository<OrderItemQuantity, Guid> _orderItemRepository;
        private readonly IAsyncQueryableExecuter _asyncExecuter;
        private readonly IDistributedEventBus _distributedEventBus;
        public OrderPlacedEventHandler(IRepository<ProductStock, Guid> productStockRepository,
            IRepository<OrderItemQuantity, Guid> orderItemRepository,
            IAsyncQueryableExecuter asyncExecuter,
            IDistributedEventBus distributedEventBus)
        {
            _productStockRepository = productStockRepository;
            _orderItemRepository = orderItemRepository;
            _asyncExecuter = asyncExecuter;
            _distributedEventBus = distributedEventBus;
        }

        [UnitOfWork]
        public virtual async Task HandleEventAsync(OrderPlacedEto eventData)
        {
            var orderItems = await _orderItemRepository.GetListAsync(o => o.OrderId == eventData.OrderId);
            var productIds = orderItems.Select(x => x.ProductId).ToList();
            var products = await _productStockRepository.GetListAsync(o => productIds.Contains(o.Id));

            List<Guid> stockNotAvailableProducts = new();
            foreach (var product in products)
            {
                var orderItem = orderItems.First(o => o.ProductId == product.Id);
                if (product.StockCount < orderItem.Quantity)
                    stockNotAvailableProducts.Add(product.Id);
            }
            if (stockNotAvailableProducts.IsNullOrEmpty())
            {
                //if everything goes right then make the discount from the stock.
                foreach (var product in products)
                {
                    var orderItem = orderItems.First(o => o.ProductId == product.Id);
                    product.UpdateStockCount(product.StockCount - orderItem.Quantity);
                }
                await _distributedEventBus.PublishAsync(new StockExistsEto() { OrderId = eventData.OrderId });

            }
            else
                await _distributedEventBus.PublishAsync(new StockNotAvailableEto() { OrderId = eventData.OrderId, ProductIds = stockNotAvailableProducts });


        }
    }
}
