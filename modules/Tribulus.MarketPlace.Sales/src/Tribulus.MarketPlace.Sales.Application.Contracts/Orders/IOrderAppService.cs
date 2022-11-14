using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Sales.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<PagedResultDto<OrderDto>> GetListAsync(OrderFilterDto input);

        Task<OrderDto> CreateAsync(CreateOrderDto input);

        Task UpdateAsync(Guid id, UpdateOrderDto input);

        Task CreateOrderItemAsync(Guid orderId, CreateOrderItemDto input);

        Task DeleteOrderItemAsync(Guid orderId,Guid orderItemId);

        Task DeleteOrderAsync(Guid id);

        Task PlaceOrderAsync(Guid id);

    }
}
