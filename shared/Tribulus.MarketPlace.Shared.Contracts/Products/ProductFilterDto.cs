using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Products;

public class ProductFilterDto:PagedResultRequestDto
{
    public string Name { get; set; }
}
