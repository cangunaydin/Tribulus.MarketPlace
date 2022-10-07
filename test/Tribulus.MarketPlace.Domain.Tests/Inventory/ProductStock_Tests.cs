using System;
using Xunit;

namespace Tribulus.MarketPlace.Inventory
{
    public class ProductStock_Tests : MarketPlaceDomainTestBase
    {
        private readonly MarketPlaceTestData _testData;

        public ProductStock_Tests()
        {
            _testData = GetRequiredService<MarketPlaceTestData>();
        }
       
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
