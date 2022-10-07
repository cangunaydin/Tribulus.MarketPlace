using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Tribulus.MarketPlace.Inventory
{
    public class OrderItemQuantity_Tests
    {
        [Fact]
        public void Should_Not_Create_With_Negative_Value()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                new OrderItemQuantity(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    -5
                );
            });
        }


    }
}
