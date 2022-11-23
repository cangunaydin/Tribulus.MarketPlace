using MediatR;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class GetProductListEto : INotification
{
    public ProductFilterDto Filter { get; set; }
    public PagedResultDto<ProductAggregateDto> Products { get; set; }
}
