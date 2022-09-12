using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderItem:Entity<Guid>
    {
        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public decimal SubTotal { get; private set; }


        private OrderItem()
        {

        }
        public OrderItem(
            Guid id,
            Guid orderId,
            Guid productId,
            decimal price,
            int quantity):base(id)
        {
            OrderId = orderId;
            ProductId = productId;
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            Price = price;
            UpdateQuantity(quantity);
        }
        public void UpdateQuantity(int quantity)
        {
            Check.Positive(quantity,nameof(quantity));
            Quantity = quantity;
            SubTotal = Quantity * Price;
        }


    }
}
