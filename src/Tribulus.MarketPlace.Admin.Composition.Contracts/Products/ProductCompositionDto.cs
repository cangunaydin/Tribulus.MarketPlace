﻿using Tribulus.Composition;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.Admin.Products
{
    public class ProductCompositionDto : CompositionViewModelBase
    {
        public ProductDto Product { get; set; }

        public ProductPriceDto ProductPrice { get; set; }

    }
}