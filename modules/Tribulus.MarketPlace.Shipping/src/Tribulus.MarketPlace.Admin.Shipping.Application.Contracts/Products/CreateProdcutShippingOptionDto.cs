using System;
using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public class CreateProdcutShippingOptionDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Option { get; set; }
        public int EstimatedMinDeliveryDays { get; set; }
        public int EstimatedMaxDeliveryDays { get; set; }
    }
}
