using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Admin.Shipping.Products
{
    public class CreateProductDeliveryDto
    {
        [Required]
        public string ShippingName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MinDays { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MaxDays { get; set; }
    }
}
