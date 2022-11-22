using Shouldly;
using System;
using System.Linq;
using Tribulus.MarketPlace.Sales.Orders.Events;
using Xunit;

namespace Tribulus.MarketPlace.Sales.Orders
{
    public class Order_Tests:SalesDomainTestBase
    {
        [Fact]
        public void Should_Not_Create_With_NullOrEmpty_Name()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                new Order(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    null
                );
            });
        }
        [Fact]
        public void Should_Add_OrderItem()
        {
            // Arrange
            var productId1 = Guid.NewGuid();
            var orderItem1Id= Guid.NewGuid();
            var order = new Order(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "test order"
                );
            // Act
            order.AddOrderItem(orderItem1Id, productId1, 100, 2);

            //Assert
            order.OrderItems.Count().ShouldBe(1);
            order.TotalValue.ShouldBe(200);
            order.OrderItems.First().Id.ShouldBe(orderItem1Id);

        }
        [Fact]
        public void Should_Not_Add_OrderItem_With_Negative_Or_Zero_StockCount()
        {
            // Arrange
            var productId1 = Guid.NewGuid();

            var order = new Order(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "test order"
                );

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                order.AddOrderItem(order.Id, productId1, 2, -1);
            });

        }
        [Fact]
        public void Should_Update_Name()
        {
            // Arrange

            var order = new Order(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "test order"
                );
            var newName = "changed name";
            // Act
            order.UpdateName(newName);
            //Assert
            order.Name.ShouldBe(newName);

        }
        [Fact]
        public void Should_PlaceOrder()
        {
            // Arrange

            var order = new Order(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "test order"
                );
           
            // Act
            order.PlaceOrder();
            //Assert
            order.State.ShouldBe(OrderState.Pending);
            order.GetDistributedEvents().Where(o => o.EventData.GetType() == typeof(OrderPlacedEto)).ShouldNotBeEmpty();

        }


    }
}
