using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderListFilterDto : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}
