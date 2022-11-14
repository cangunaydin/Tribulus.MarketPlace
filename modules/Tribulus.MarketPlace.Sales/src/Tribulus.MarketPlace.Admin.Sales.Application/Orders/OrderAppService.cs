using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Permissions;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Sales.Orders
{
    [Authorize(SalesPermissions.Orders.Default)]
    public class OrderAppService : AdminSalesAppService, IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderAppService( IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
    }
}
