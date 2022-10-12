using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Shipping
{
    public class CreateProductShippingOptionDto : EntityDto<Guid>
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Option { get; set; }
        public int EstimatedMinDeliveryDays { get; set; }
        public int EstimatedMaxDeliveryDays { get; set; }
    }
}
