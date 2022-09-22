using System;
using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Orders
{
    public class CreateOrderItemDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "The field {0} must be greater than or equal to {1}.")]
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
