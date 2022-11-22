using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Products.Events;
using Tribulus.MarketPlace.Sales.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.Inventory.EventHandlers
{
    public class StockExistsEventHandler : IDistributedEventHandler<StockExistsEto>,
          ITransientDependency
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        public StockExistsEventHandler(IRepository<Order, Guid> orderRepository, IDistributedEventBus distributedEventBus)
        {
            _orderRepository = orderRepository;
            _distributedEventBus = distributedEventBus;
        }

        [UnitOfWork]
        public virtual async Task HandleEventAsync(StockExistsEto eventData)
        {
            var order=await _orderRepository.GetAsync(eventData.OrderId);
            order.ConfirmOrder();
        }
    }
}
