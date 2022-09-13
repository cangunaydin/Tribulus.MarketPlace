using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin.Products
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(ProductConsts.MaxNameLength,MinimumLength =ProductConsts.MinNameLength)]
        public string Name { get; set; }

        [CanBeNull]
        [StringLength(ProductConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int StockCount { get; set; }
    }
}
