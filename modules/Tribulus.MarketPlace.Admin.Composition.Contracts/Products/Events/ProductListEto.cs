using MediatR;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Products.Events;

public class ProductListEto : INotification
{
    public ProductFilterDto Filter { get; set; }
    public PagedResultDto<ProductCompositionDto> Products { get; set; }
}
