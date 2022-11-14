using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Orders;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Admin.Sales.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<PagedResultDto<OrderDto>> GetListAsync(OrderFilterDto input);
    }
}
