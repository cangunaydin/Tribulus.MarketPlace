using AutoMapper;
using System.Collections.Generic;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin;

public class AdminApplicationAutoMapperProfile : Profile
{
    public AdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Product, ProductDto>();
        CreateMap<List<Product>, List<ProductDto>>();
    }
}
