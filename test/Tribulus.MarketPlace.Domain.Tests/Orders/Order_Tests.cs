using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Tribulus.MarketPlace.Orders
{
    public class Order_Tests
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
        //[Fact]
        //public async Task Should_Not_Place_Order_Without_Product_StockCount()
        //{
        //    // Arrange
        //    var productId1 = Guid.NewGuid();
        //    var product1 = new Product(productId1, Guid.NewGuid(), "Product 1", 1000, 2);
        //    var orderItem1Id = Guid.NewGuid();
        //    var order = new Order(
        //            Guid.NewGuid(),
        //            Guid.NewGuid(),
        //            "test order"
        //        );

        //    order.AddOrderItem(orderItem1Id, productId1, 1000, 5);

        //    var productRepository =
        //        Substitute.For<IRepository<Product, Guid>>();

        //    productRepository.GetListAsync(Arg.Any<Expression<Func<Product, bool>>>())
        //        .Returns(Task.FromResult(new List<Product>() { product1 }));

        //    var orderManager = new OrderManager(productRepository);
        //    await Assert.ThrowsAsync<BusinessException>(async () =>
        //    {
        //        await orderManager.PlaceOrderAsync(order);
        //    });
            

        //}


    }
}
