﻿using System;

namespace Tribulus.MarketPlace.Products;

public class ProductCompositionDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockCount { get; set; }
}