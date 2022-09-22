using AutoMapper;
using Tribulus.MarketPlace.Orders;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace;

public class MarketPlaceApplicationAutoMapperProfile : Profile
{
    public MarketPlaceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Product, ProductDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();

    }
}
