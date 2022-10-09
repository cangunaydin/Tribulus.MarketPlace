using System;
using Tribulus.MarketPlace.Sales.Products;
using Xunit;

namespace Tribulus.MarketPlace.Sales
{
    public class ProductPrice_Tests : SalesDomainTestBase
    {

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
