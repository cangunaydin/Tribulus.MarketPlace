using AutoMapper;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.Sales;

public class SalesApplicationAutoMapperProfile : Profile
{
    public SalesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductPrice, ProductPriceDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}
