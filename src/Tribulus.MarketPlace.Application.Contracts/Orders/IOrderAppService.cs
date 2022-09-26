using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Orders
{
    public interface IOrderAppService:IApplicationService
    {
        Task<PagedResultDto<OrderDto>> GetOrdersAsync(OrderFilterDto input);

        Task<OrderDto> GetOrderByUserIdAsync(Guid userId);

        Task<OrderDto> CreateAsync(CreateOrderDto input);

        Task<OrderDto> UpdateAsync(Guid id,UpdateOrderDto input);

        Task<OrderItemDto> CreateOrderItemAsync(Guid orderId,CreateOrderItemDto input);

        Task DeleteOrderItem(Guid orderItemId, Guid orderId);

        Task DeleteOrder(Guid id);

        Task PlaceOrder(Guid id);

    }
}
