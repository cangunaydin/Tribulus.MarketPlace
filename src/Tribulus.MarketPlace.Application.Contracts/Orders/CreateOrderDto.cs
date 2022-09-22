using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Orders
{
    public class CreateOrderDto
    {
        [Required]
        public string Name { get; set; }

    }
}
