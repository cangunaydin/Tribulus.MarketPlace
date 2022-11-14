using MediatR;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class ProductListEto : INotification
{
    public ProductFilterDto Filter { get; set; }
    public PagedResultDto<ProductCompositionDto> Products { get; set; }
}
