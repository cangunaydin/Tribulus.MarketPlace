using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Tribulus.MarketPlace.Inventory.Orders
{
    public class OrderItemQuantity : AggregateRoot<Guid>
    {
        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }



        private OrderItemQuantity()
        {

        }
        public OrderItemQuantity(
            Guid id,
            Guid orderId,
            Guid productId,
            int quantity) : base(id)
        {
            OrderId = orderId;
            ProductId = productId;

            UpdateQuantity(quantity);
        }
        public void UpdateQuantity(int quantity)
        {
            Check.Positive(quantity, nameof(quantity));
            Quantity = quantity;
        }


    }
}
