using AutoMapper;
using Tribulus.MarketPlace.Shipping.Products;

namespace Tribulus.MarketPlace.Admin.Shipping;

public class AdminShippingApplicationAutoMapperProfile : Profile
{
    public AdminShippingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductDelivery, ProductDeliveryDto>();

    }
}
