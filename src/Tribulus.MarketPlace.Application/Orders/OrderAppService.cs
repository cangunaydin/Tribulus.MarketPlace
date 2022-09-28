using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Orders
{
    [Authorize]
    public class OrderAppService : MarketPlaceAppService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly OrderManager _orderManager;

        public OrderAppService(IRepository<Order, Guid> orderRepository, OrderManager orderManager)
        {
            _orderRepository = orderRepository;
            _orderManager = orderManager;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = new Order(GuidGenerator.Create(), CurrentUser.GetId(), input.Name);
            await _orderRepository.InsertAsync(order, true);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        public async Task<OrderItemDto> CreateOrderItemAsync(Guid orderId, CreateOrderItemDto input)
        {   
             var order = await _orderRepository.GetAsync(orderId);
             var guid = GuidGenerator.Create();
             order.AddOrderItem(guid, input.ProductId, input.Price, input.Quantity);
             var orderitem = order.OrderItems.Where(x => x.Id == guid).FirstOrDefault();
             return ObjectMapper.Map<OrderItem, OrderItemDto>(orderitem);           
        }

        public async Task DeleteOrder(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            await _orderRepository.DeleteAsync(order, true);
        }

        public async Task DeleteOrderItem(Guid orderItemId, Guid orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            order.RemoveOrderItem(orderItemId);
        }

        public async Task<OrderDto> GetCurrentOrderAsync()
        {
            var query = await _orderRepository.WithDetailsAsync(x => x.OrderItems);
            query = query.Where(o => o.OwnerUserId == CurrentUser.GetId());

            var ordersCount = await AsyncExecuter.CountAsync(query);
            var order = await AsyncExecuter.ToListAsync(query);
            var orderDto = ObjectMapper.Map<Order, OrderDto>(order.FirstOrDefault());
            return orderDto;
        }

        public async Task<PagedResultDto<OrderDto>> GetOrdersAsync(OrderFilterDto input)
        {
            var query = await _orderRepository.WithDetailsAsync(x=>x.OrderItems);
            query = query.Where(o => o.OwnerUserId == CurrentUser.GetId());
            query = query.PageBy(input);

            var ordersCount = await AsyncExecuter.CountAsync(query);    
            var orders = await AsyncExecuter.ToListAsync(query);
            var ordersDto = ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);
            return new PagedResultDto<OrderDto>(ordersCount, ordersDto);

        }

        public async Task PlaceOrder(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            await _orderManager.PlaceOrderAsync(order);
        }

        public async Task UpdateAsync(Guid id, UpdateOrderDto input)
        {
            var order = await _orderRepository.GetAsync(id);
            order.UpdateName(input.Name);
            await _orderRepository.UpdateAsync(order);
        }
    }
}
