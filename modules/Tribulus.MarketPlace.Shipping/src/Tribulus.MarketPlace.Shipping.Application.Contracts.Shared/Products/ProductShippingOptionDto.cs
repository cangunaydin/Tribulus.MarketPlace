using System;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductShippingOptionDto: EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public string Option { get; set; }
        public int EstimatedMinDeliveryDays { get; set; }
        public int EstimatedMaxDeliveryDays { get; set; }
    }
}
