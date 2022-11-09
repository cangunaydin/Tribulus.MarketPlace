using AutoMapper;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Products;
using Volo.Abp.AutoMapper;

namespace Tribulus.MarketPlace.Admin.Marketing;

public class AdminMarketingCompositionAutoMapperProfile : Profile
{
    public AdminMarketingCompositionAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductDto, ProductCompositionDto>()
            .Ignore(o => o.Price)
            .Ignore(o => o.StockCount);
        
    }
}
