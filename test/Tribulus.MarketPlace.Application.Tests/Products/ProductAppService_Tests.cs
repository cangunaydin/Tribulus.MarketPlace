using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Orders;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Products
{
    public class ProductAppService_Tests : MarketPlaceApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        private readonly MarketPlaceTestData _testData;

        public ProductAppService_Tests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
            _testData = GetRequiredService<MarketPlaceTestData>();
        }



        [Fact]
        public async Task Should_Get_List_Of_Products()
        {
            ProductListFilterDto filter = new ProductListFilterDto();
            var result = await _productAppService.GetListAsync(filter);
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(x => x.Name == _testData.ProductIphone13Name);
        }



    }
}
