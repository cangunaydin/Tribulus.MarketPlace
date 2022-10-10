using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Products.Handlers
{
    public class HandleGetProductListEto : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
