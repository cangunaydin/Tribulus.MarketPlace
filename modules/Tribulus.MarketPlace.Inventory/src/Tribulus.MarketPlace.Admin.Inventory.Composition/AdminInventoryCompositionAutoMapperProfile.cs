using AutoMapper;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Products;
using Volo.Abp.AutoMapper;

namespace Tribulus.MarketPlace.Admin.Inventory;

public class AdminInventoryCompositionAutoMapperProfile : Profile
{
    public AdminInventoryCompositionAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductStockDto, ProductCompositionDto>()
            .Ignore(o => o.Name)
            .Ignore(o => o.Description)
            .Ignore(o => o.Price);
        
    }
}
