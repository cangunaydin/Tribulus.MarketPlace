using AutoMapper;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;

namespace Tribulus.MarketPlace.Admin.Sales;

public class AdminSalesApplicationAutoMapperProfile : Profile
{
    public AdminSalesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductPrice, ProductPriceDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        
    }
}
