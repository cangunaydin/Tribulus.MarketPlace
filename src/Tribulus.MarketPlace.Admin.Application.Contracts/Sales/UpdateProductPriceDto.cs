using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin.Sales
{
    public class UpdateProductPriceDto
    {

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
