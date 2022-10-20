using AutoMapper;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.AutoMapper;

namespace Tribulus.MarketPlace.Admin.Sales;

public class AdminSalesCompositionAutoMapperProfile : Profile
{
    public AdminSalesCompositionAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductPriceDto, ProductCompositionDto>()
            .Ignore(o => o.Name)
            .Ignore(o => o.Description)
            .Ignore(o => o.StockCount)
            .Ignore(o => o.ShippingName);
        

    }
}
