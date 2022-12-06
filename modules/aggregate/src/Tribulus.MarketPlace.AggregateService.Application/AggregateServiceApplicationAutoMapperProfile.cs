using AutoMapper;
using Tribulus.MarketPlace.Sales.Products;
using Tribulus.MarketPlace.AggregateService.Products;
using Volo.Abp.AutoMapper;
using Tribulus.MarketPlace.Marketing.Products;
using Tribulus.MarketPlace.Inventory.Products;

namespace Tribulus.MarketPlace.AggregateService;

public class AggregateServiceApplicationAutoMapperProfile : Profile
{
    public AggregateServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductPriceDto, ProductAggregateDto>()
            .Ignore(x => x.Name)
            .Ignore(x => x.Description)
            .Ignore(x => x.StockCount);
            

        CreateMap<ProductDto, ProductAggregateDto>()
            .Ignore(x => x.Price)
            .Ignore(x => x.StockCount);

        CreateMap<ProductStockDto, ProductAggregateDto>()
            .Ignore(x => x.Name)
            .Ignore(x => x.Description)
            .Ignore(x => x.Price);

        CreateMap<ProductAggregateFilterDto, ProductListFilterDto>();


    }
}
