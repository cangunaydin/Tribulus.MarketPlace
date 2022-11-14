using AutoMapper;
using Tribulus.MarketPlace.Inventory.Products;

namespace Tribulus.MarketPlace.Admin.Inventory;

public class AdminInventoryApplicationAutoMapperProfile : Profile
{
    public AdminInventoryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductStock, ProductStockDto>();
    }
}
