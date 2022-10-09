using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Orders.Events;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Sales.Orders
{
    public class OrderAppService_Tests : SalesApplicationTestBase
    {
        private readonly IOrderAppService _orderAppService;
        private ICurrentUser _currentUser;
        private readonly SalesTestData _salesTestData;
        private readonly IOrderRepository _orderRepository;
        public OrderAppService_Tests()
        {
            _orderAppService = GetRequiredService<IOrderAppService>();
            _salesTestData = GetRequiredService<SalesTestData>();
            _orderRepository = GetRequiredService<IOrderRepository>();
        }
        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);

        }
        [Fact]
        public async Task Should_Get_Orders_WithSearch()
        {
            Login(_salesTestData.UserJohnId);
            string searchName = "Order 1";
            var ordersResult = await _orderAppService.GetListAsync(new OrderFilterDto() { Name = searchName });
            ordersResult.Items.Count.ShouldBe(1);
        }
        [Fact]
        public async Task Should_Create_Valid_Order()
        {
            Login(_salesTestData.UserJohnId);
            var orderName = "Test Order";
            var createOrderDto = new CreateOrderDto()
            {
                Name = orderName
            };
            var orderDto = await _orderAppService.CreateAsync(createOrderDto);
            orderDto.ShouldNotBeNull();
            orderDto.Name.ShouldBe(orderName);
            var order = await GetOrderOrNullAsync(orderDto.Id);
            order.ShouldNotBeNull();
        }
        [Fact]
        public async Task Should_Create_Valid_OrderItem()
        {
            Login(_salesTestData.UserJohnId);
            var newOrderItemDto = new CreateOrderItemDto();
            newOrderItemDto.Price = 100;
            newOrderItemDto.ProductId = _salesTestData.ProductIphone14Id;
            newOrderItemDto.Quantity = 2;

            await _orderAppService.CreateOrderItemAsync(_salesTestData.Order1Id, newOrderItemDto);
            var order = await GetOrderOrNullAsync(_salesTestData.Order1Id);
            order.ShouldNotBeNull();
            order.OrderItems.Count().ShouldBe(2);
            order.OrderItems.Last().Price.ShouldBe(100);
            order.OrderItems.Last().ProductId.ShouldBe(_salesTestData.ProductIphone14Id);
            order.OrderItems.Last().Quantity.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Delete_Order()
        {
            Login(_salesTestData.UserJohnId);
            await _orderAppService.DeleteOrderAsync(_salesTestData.Order1Id);
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await GetOrderOrNullAsync(_salesTestData.Order1Id);
            });

        }
        [Fact]
        public async Task Should_Delete_OrderItem()
        {
            Login(_salesTestData.UserJohnId);
            await _orderAppService.DeleteOrderItemAsync(_salesTestData.Order1Id, _salesTestData.Order1ItemId);
            var order = await GetOrderOrNullAsync(_salesTestData.Order1Id);
            order.ShouldNotBeNull();
            order.OrderItems.Count().ShouldBe(0);
        }
        [Fact]
        public async Task Should_Update_Order()
        {
            Login(_salesTestData.UserJohnId);
            var orderName = "Order 1 Updated";
            await _orderAppService.UpdateAsync(_salesTestData.Order1Id, new UpdateOrderDto() { Name = orderName });
            var order = await GetOrderOrNullAsync(_salesTestData.Order1Id);
            order.Name.ShouldBe(orderName);
        }
        [Fact]
        public async Task Should_Place_Order()
        {
            Login(_salesTestData.UserJohnId);
            await _orderAppService.PlaceOrderAsync(_salesTestData.Order1Id);
            var order = await GetOrderOrNullAsync(_salesTestData.Order1Id);
            order.State.ShouldBe(OrderState.Pending);
            //order.GetDistributedEvents().Where(o => o.GetType() == typeof(OrderPlacedEto)).ShouldNotBeEmpty();
        }
        private async Task<Order> GetOrderOrNullAsync(Guid orderId)
        {
            return await WithUnitOfWorkAsync(async () =>
            {
                return await _orderRepository.GetAsync(orderId);
            });
        }
        private void Login(Guid userId)
        {
            _currentUser.Id.Returns(userId);
            _currentUser.IsAuthenticated.Returns(true);
        }
    }
}
