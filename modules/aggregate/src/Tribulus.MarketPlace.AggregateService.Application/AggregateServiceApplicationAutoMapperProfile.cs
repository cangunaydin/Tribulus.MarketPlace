using AutoMapper;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.AggregateService;

public class AggregateServiceApplicationAutoMapperProfile : Profile
{
    public AggregateServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<CreateProductAggregateDto, CreateProductDto>();
        CreateMap<CreateProductAggregateDto, CreateProductStockDto>();
        CreateMap<CreateProductAggregateDto, CreateProductPriceDto>();

    }
}
