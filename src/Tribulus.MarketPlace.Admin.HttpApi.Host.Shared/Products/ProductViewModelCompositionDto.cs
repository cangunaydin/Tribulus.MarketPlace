﻿using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Sales.Products;
using Tribulus.MarketPlace.Shipping.Products;

namespace Tribulus.MarketPlace.Admin.Products
{
    public class ProductViewModelCompositionDto 
    {
        public ProductDto Product { get; set; }
        public ProductPriceDto ProductPrice { get; set; }
        public ProductStockDto ProductStock { get; set; }
        public ProductShippingOptionDto ProductShippingOption { get; set; }

    }
}