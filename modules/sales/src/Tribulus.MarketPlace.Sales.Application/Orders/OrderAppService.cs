using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Sales.Orders
{
    [Authorize(SalesPermissions.Orders.Default)]
    public class OrderAppService : SalesAppService, IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderAppService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [Authorize(SalesPermissions.Orders.Create)]
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = new Order(GuidGenerator.Create(), CurrentUser.GetId(), input.Name);
            await _orderRepository.InsertAsync(order);
            return ObjectMapper.Map<Order,OrderDto>(order);
        }

        [Authorize(SalesPermissions.Orders.Create)]
        public async Task CreateOrderItemAsync(Guid orderId, CreateOrderItemDto input)
        {
            var order = await _orderRepository.GetAsync(orderId);
            order.AddOrderItem(GuidGenerator.Create(), input.ProductId, input.Price, input.Quantity);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            //check if it has the same ownerid as logged in user.
            await _orderRepository.DeleteAsync(id);
        }

        public async Task DeleteOrderItemAsync(Guid orderId,Guid orderItemId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            order.RemoveOrderItem(orderItemId);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task<PagedResultDto<OrderDto>> GetListAsync(OrderFilterDto input)
        {
            var query = await _orderRepository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Name), o => o.Name.Contains(input.Name));
            query = query.PageBy(input);

            var ordersCount = await AsyncExecuter.CountAsync(query);
            var orders = await AsyncExecuter.ToListAsync(query);
            var ordersDto = ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);

            return new PagedResultDto<OrderDto>(ordersCount, ordersDto);
        }

        public async Task PlaceOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            order.PlaceOrder();
        }
        [Authorize(SalesPermissions.Orders.Update)]
        public async Task UpdateAsync(Guid id, UpdateOrderDto input)
        {
            var order = await _orderRepository.GetAsync(id);
            order.UpdateName(input.Name);
            await _orderRepository.UpdateAsync(order);
        }
    }
}
