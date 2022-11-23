﻿using AutoMapper;
using Tribulus.MarketPlace.Inventory.Products;

namespace Tribulus.MarketPlace.Inventory;

public class InventoryApplicationAutoMapperProfile : Profile
{
    public InventoryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductStock, ProductStockDto>();
    }
}