using AutoMapper;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.AutoMapper;

namespace Tribulus.MarketPlace.Admin.Shipping;

public class AdminShippingCompositionAutoMapperProfile : Profile
{
    public AdminShippingCompositionAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductDeliveryDto, ProductCompositionDto>()
            .Ignore(o => o.Name)
            .Ignore(o => o.Description)
            .Ignore(o => o.Price)
            .Ignore(o=>o.StockCount);

       


    }
}
