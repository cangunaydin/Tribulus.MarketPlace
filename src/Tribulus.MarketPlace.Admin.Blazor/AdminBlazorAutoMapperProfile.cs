﻿using AutoMapper;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Blazor;

public class AdminBlazorAutoMapperProfile : Profile
{
    public AdminBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<ProductDto, UpdateProductDto>();
    }
}
