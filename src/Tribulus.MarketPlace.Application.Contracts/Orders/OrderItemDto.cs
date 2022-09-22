using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderItemDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
