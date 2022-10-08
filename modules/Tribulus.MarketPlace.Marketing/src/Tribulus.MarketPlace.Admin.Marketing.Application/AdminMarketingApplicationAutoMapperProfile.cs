using AutoMapper;
using Tribulus.MarketPlace.Marketing.Products;

namespace Tribulus.MarketPlace.Admin.Marketing;

public class AdminMarketingApplicationAutoMapperProfile : Profile
{
    public AdminMarketingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Product, ProductDto>();
    }
}
