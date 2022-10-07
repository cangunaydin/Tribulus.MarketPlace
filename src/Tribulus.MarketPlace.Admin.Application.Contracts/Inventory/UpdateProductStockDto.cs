using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin.Inventory
{
    public class UpdateProductStockDto
    {
        [Required]
        [Range(0,int.MaxValue)]
        public int StockCount { get; set; }
    }
}
