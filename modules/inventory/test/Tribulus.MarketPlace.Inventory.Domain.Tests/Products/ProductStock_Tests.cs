using System;
using Xunit;

namespace Tribulus.MarketPlace.Inventory.Products
{
    public class ProductStock_Tests : InventoryDomainTestBase
    {
        [Fact]
        public void Should_Not_Create_With_Negative_StockCount()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                new ProductStock(
                    Guid.NewGuid(),
                    -5
                );
            });
        }
    }
}
