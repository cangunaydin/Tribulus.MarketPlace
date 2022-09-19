using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderDto:EntityDto<Guid>
    {
        public string Name { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
