using System;
using Xunit;

namespace Tribulus.MarketPlace.Sales
{
    public class ProductPrice_Tests : MarketPlaceDomainTestBase
    {
        private readonly MarketPlaceTestData _testData;

        public ProductPrice_Tests()
        {
            _testData = GetRequiredService<MarketPlaceTestData>();
        }
        [Fact]
        public void Should_Not_Create_With_Negative_Price()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new ProductPrice(
                    Guid.NewGuid(),
                    -10
                );
            });
        }
    }
}
