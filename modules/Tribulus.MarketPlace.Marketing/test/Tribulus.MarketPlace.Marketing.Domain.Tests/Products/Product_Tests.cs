using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tribulus.MarketPlace.Marketing.Products
{
    public class Product_Tests : MarketingDomainTestBase
    {
        private readonly MarketingTestData _testData;

        public Product_Tests()
        {
            _testData = GetRequiredService<MarketingTestData>();
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
                    null
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
                    "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss"
                );
            });
        }

    }
}
