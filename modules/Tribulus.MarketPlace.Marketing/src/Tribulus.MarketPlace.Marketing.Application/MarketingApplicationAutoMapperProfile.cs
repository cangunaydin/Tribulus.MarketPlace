using AutoMapper;
using Tribulus.MarketPlace.Marketing.Products;

namespace Tribulus.MarketPlace.Marketing;

public class MarketingApplicationAutoMapperProfile : Profile
{
    public MarketingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Product, ProductDto>();
    }
}
