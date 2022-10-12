using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductShippingOptions : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; set; }
        public string Option { get; set; }
        public int EstimatedMinDeliveryDays { get; set; }
        public int EstimatedMaxDeliveryDays { get; set; }

        public ProductShippingOptions(Guid id, Guid productId, string option, int estimatedMinDeliveryDays, int estimatedMaxDeliveryDays) : base(id)
        {
            ProductId = productId;
            Option = option;
            EstimatedMinDeliveryDays = estimatedMinDeliveryDays;
            EstimatedMaxDeliveryDays = estimatedMaxDeliveryDays;
        }



    }
}
