using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Admin.Inventory
{
    public class UpdateProductStockDto
    {
        [Required]
        [Range(0,int.MaxValue)]
        public int StockCount { get; set; }
    }
}
