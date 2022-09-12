using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Orders.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;

namespace Tribulus.MarketPlace.Products.EventHandlers
{
    public class OrderPlacedEventHandler : ILocalEventHandler<PlaceOrderEventData>,
          ITransientDependency
    {
        private readonly IRepository<Product,Guid> _productRepository;
        public OrderPlacedEventHandler(IRepository<Product,Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleEventAsync(PlaceOrderEventData eventData)
        {
            var order = eventData.Order;

            foreach (var orderItem in order.OrderItems)
            {
                var product = await _productRepository.GetAsync(o => o.Id == orderItem.ProductId);
                product.UpdateStockCount(product.StockCount - orderItem.Quantity);
            }

        }
    }
}
