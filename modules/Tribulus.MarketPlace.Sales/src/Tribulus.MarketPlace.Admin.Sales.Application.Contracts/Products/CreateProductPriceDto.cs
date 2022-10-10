﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public class CreateProductPriceDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}