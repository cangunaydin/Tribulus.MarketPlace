using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Admin.Sales
{
    public class CreateProductPriceDto
    {

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
