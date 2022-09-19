using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Tribulus.MarketPlace.Orders.Events;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Orders
{
    public class Order:FullAuditedAggregateRoot<Guid>
    {

        public Guid OwnerUserId { get; private set; }
        public string Name { get; private set; }

        public decimal TotalValue { get; private set; }

        public OrderState State { get; private set; }

        public ICollection<OrderItem> OrderItems { get; private set; }


        private Order()
        {

        }

        internal Order(Guid id,Guid ownerUserId,string name):base(id)
        {
            OwnerUserId = ownerUserId;
            State = OrderState.Pending;
            UpdateName(name);
            OrderItems = new Collection<OrderItem>();
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
            CheckOrderItemNotExists(orderItemId);
            var newOrderItem = new OrderItem(orderItemId, Id, productId, price, quantity);
            OrderItems.Add(newOrderItem);
            UpdateTotalValue();

            return this;
        }
        public Order UpdateOrderItemQuantity(Guid orderItemId,int quantity)
        {
            CheckOrderItemExists(orderItemId);
            var orderItem = OrderItems.First(o => o.Id == orderItemId);
            orderItem.UpdateQuantity(quantity);
            UpdateTotalValue();
            return this;
        }
        public Order RemoveOrderItem(Guid orderItemId)
        {
            CheckOrderItemExists(orderItemId);
            var orderItem = OrderItems.First(o => o.Id == orderItemId);
            OrderItems.Remove(orderItem);
            UpdateTotalValue();
            return this;
        }
        private void CheckOrderItemExists(Guid orderItemId)
        {
            if (!OrderItems.Any(o => o.Id == orderItemId))
                throw new ArgumentNullException(nameof(orderItemId));
        }
        private void CheckOrderItemNotExists(Guid orderItemId)
        {
            if (OrderItems.Any(o => o.Id == orderItemId))
                throw new ArgumentNullException(nameof(orderItemId));
        }
        private void UpdateTotalValue()
        {
            decimal totalValue = 0;
            foreach (var orderItem in OrderItems)
            {

                totalValue += orderItem.SubTotal;
            }
            TotalValue= totalValue;
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
