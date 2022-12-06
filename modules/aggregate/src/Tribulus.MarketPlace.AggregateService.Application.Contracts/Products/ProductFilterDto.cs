using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.AggregateService.Products;

public class ProductAggregateFilterDto:PagedResultRequestDto
{
    public string Name { get; set; }
}
