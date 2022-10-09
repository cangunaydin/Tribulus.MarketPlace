using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Events;
using Tribulus.MarketPlace.Sales.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Tribulus.MarketPlace.Inventory.EventHandlers
{
    public class StockNotAvailableEventHandler : IDistributedEventHandler<StockNotAvailableEto>,
          ITransientDependency
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        public StockNotAvailableEventHandler(IRepository<Order, Guid> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [UnitOfWork]
        public virtual async Task HandleEventAsync(StockNotAvailableEto eventData)
        {
            var order=await _orderRepository.GetAsync(eventData.OrderId);
            order.RejectOrder();

            //send an email to the user.
        }
    }
}
