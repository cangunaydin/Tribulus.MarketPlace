using System;
using Xunit;

namespace Tribulus.MarketPlace.Products
{
    public  class Product_Tests : MarketPlaceDomainTestBase
    {
        private readonly MarketPlaceTestData _testData;

        public Product_Tests()
        {
            _testData = GetRequiredService<MarketPlaceTestData>();
        }
        [Fact]
        public void Should_Not_Create_With_NullOrEmpty_Name()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new Product(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    null,
                    100,
                    5
                );
            });
        }
        [Fact]
        public void Should_Not_Create_With_Name_LessThan_2_MoreThan_256_Chars()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new Product(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd" +
                    "ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd" +
                    "dddddddddddddddddddddafasdfsssssssssssssssssssssssssssssssssssssssssssssssss" +
                    "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss",
                    100,
                    5
                );
            });
        }
        [Fact]
        public void Should_Not_Create_With_Negative_StockCount()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                new Product(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "name - 1",
                    100,
                    -5
                );
            });
        }
    }
}
