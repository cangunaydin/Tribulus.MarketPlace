using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Products
{
    public class ProductFilterDto : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
