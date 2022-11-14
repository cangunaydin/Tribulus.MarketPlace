using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public class UpdateProductPriceDto
    {

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
