using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderAppService_Tests : MarketPlaceApplicationTestBase
    {
        private readonly IOrderAppService _orderAppService;
        private readonly MarketPlaceTestData _testData;
        private ICurrentUser _currentUser;

        public OrderAppService_Tests()
        {
            _orderAppService = GetRequiredService<IOrderAppService>();
            _testData = GetRequiredService<MarketPlaceTestData>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);
        }


        [Fact]
        public async Task Should_Get_List_Of_Orders()
        {
            OrderFilterDto filter = new OrderFilterDto();
            var result = await _orderAppService.GetOrdersAsync(filter);
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(x => x.Name == _testData.Order1Name);
        }

        [Fact]
        public async Task Should_Create_A_Valid_Order()
        {
            Login(_testData.UserAdminId);
            var testOrderName = "test-order-1";
            await _orderAppService.CreateAsync(
                new CreateOrderDto
                {
                    Name = testOrderName,
                }
            );

            var orders = await _orderAppService.GetOrdersAsync(new OrderFilterDto()
            {
                Name = testOrderName
            });
            orders.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            orders.Items.ShouldContain(x => x.Name == testOrderName);
        }

        [Fact]
        public async Task Should_Update_Order_Name()
        {
            // Arrange
            var changeOrderName = "test-update-order-data-changed";
            var updateName = new UpdateOrderDto() { Name = changeOrderName };
            var orderId = _testData.Order2Id;
            // Act
            var updatedOrder = await _orderAppService.UpdateAsync(orderId, updateName);
            //Assert
            updatedOrder.ShouldNotBeNull();
            updatedOrder.Name.ShouldContain(changeOrderName);

        }

        [Fact]
        public async Task Should_Create_A_Valid_Order_Item()
        {
            Login(_testData.UserAdminId);

            var orderItem = await _orderAppService.CreateOrderItemAsync(_testData.Order1Id, new CreateOrderItemDto
            {
                OrderId = _testData.Order1Id,
                Price = _testData.ProductIphone14Price,
                ProductId = _testData.ProductIphone14Id,
                Quantity = _testData.OrderItem1Quantity,
            });


            orderItem.Quantity.ShouldBe(_testData.OrderItem1Quantity);
            orderItem.ShouldNotBeNull();

        }

        [Fact]
        public async Task Should_Filter_Orders_By_Name()
        {
            Login(_testData.UserAdminId);
            string searchName = _testData.Order1Name;
            var ordersResult = await _orderAppService.GetOrdersAsync(new OrderFilterDto() { Name = searchName });
            foreach (var order in ordersResult.Items)
            {
                order.Name.ShouldContain(searchName);
            }
        }


        [Fact]
        public async Task Should_Delete_A_Valid_Order()
        {
            Login(_testData.UserAdminId);

            await _orderAppService.DeleteOrder(_testData.Order1Id);
            var orders = await _orderAppService.GetOrdersAsync(new OrderFilterDto() { Name = _testData.Order1Name });
            orders.TotalCount.ShouldBeLessThanOrEqualTo(0);
        }


        private void Login(Guid userId)
        {
            _currentUser.Id.Returns(userId);
            _currentUser.IsAuthenticated.Returns(true);
        }
    }
}
