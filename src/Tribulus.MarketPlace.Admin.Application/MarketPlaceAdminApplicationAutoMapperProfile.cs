using AutoMapper;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Sales;

namespace Tribulus.MarketPlace.Admin;

public class MarketPlaceAdminApplicationAutoMapperProfile : Profile
{
    public MarketPlaceAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Product, ProductDto>();
        CreateMap<ProductPrice, ProductPriceDto>();
        CreateMap<ProductStock, ProductStockDto>();
    }
}
