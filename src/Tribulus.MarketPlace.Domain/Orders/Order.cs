using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Orders.Events;
using Tribulus.MarketPlace.Products.Events;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Orders
{
    public class Order:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public decimal TotalValue { get; private set; }

        public OrderState State { get; private set; }

        public ICollection<OrderItem> OrderItems { get; private set; }


        private Order()
        {

        }

        public Order(Guid id,string name):base(id)
        {
            UpdateName(name);
        }
        public Order UpdateName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            return this;
        }

        public Order AddOrderItem(
            Guid orderItemId,
            Guid productId,
            decimal price,
            int quantity)
        {

            var newOrderItem = new OrderItem(orderItemId, Id, productId, price, quantity);
            OrderItems.Add(newOrderItem);
            return this;
        }

        public Order RemoveOrderItem(Guid orderItemId)
        {
            CheckIfOrderItemExists(orderItemId);
            var orderItem = OrderItems.First(o => o.Id == orderItemId);
            OrderItems.Remove(orderItem);
            return this;
        }
        private void CheckIfOrderItemExists(Guid orderItemId)
        {
            if (!OrderItems.Any(o => o.Id == orderItemId))
                throw new ArgumentNullException(nameof(orderItemId));
        }
        internal void PlaceOrder()
        {
            State = OrderState.Confirmed;
            //ADD an EVENT TO BE PUBLISHED
            AddLocalEvent(
                new PlaceOrderEventData
                {
                    Order = this
                }
            );
        }

    }
}
