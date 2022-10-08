using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class ProductAppServiceTests : AdminMarketingApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        private ICurrentUser _currentUser;
        private readonly MarketingTestData _marketingTestData;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppServiceTests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
            _currentUser = GetRequiredService<ICurrentUser>();
            _marketingTestData = GetRequiredService<MarketingTestData>();
            _productRepository = GetRequiredService<IRepository<Product, Guid>>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);
        }
        [Fact]
        public async Task Should_Create_New_Valid_Product()
        {
            Login(_marketingTestData.UserJohnId);

            var productDto = await _productAppService.CreateAsync(new CreateProductDto()
            {
                Description = "New Product Description",
                Name = "New Product Name"
            });
            productDto.Description.ShouldBe("New Product Description");
            productDto.Name.ShouldBe("New Product Name");
        }
        [Fact]
        public async Task Should_Update_Product()
        {
            Login(_marketingTestData.UserJohnId);
            await _productAppService.UpdateAsync(_marketingTestData.ProductIphone13Id, new UpdateProductDto()
            {
                Name = "Updated Iphone 13 Name",
                Description = "Updated Iphone 13 Description"
            });
            var updatedProduct = await GetProductOrNullAsync(_marketingTestData.ProductIphone13Id);
            updatedProduct.Name.ShouldBe("Updated Iphone 13 Name");
            updatedProduct.Description.ShouldBe("Updated Iphone 13 Description");
        }
        [Fact]
        public async Task Should_Get_Valid_Product()
        {
            Login(_marketingTestData.UserJohnId);
            var product = await _productAppService.GetAsync(_marketingTestData.ProductIphone13Id);
            product.ShouldNotBeNull();
            product.Name.ShouldBe(_marketingTestData.ProductIphone13Name);

        }

        [Fact]
        public async Task Should_Filter_Products_By_Name()
        {
            Login(_marketingTestData.UserJohnId);
            string searchName = "iphone";
            var productsResult = await _productAppService.GetListAsync(new ProductListFilterDto() { Name = searchName });
            foreach (var product in productsResult.Items)
            {
                product.Name.ShouldContain(searchName);
            }
        }
        private async Task<Product> GetProductOrNullAsync(Guid productId)
        {
            return await WithUnitOfWorkAsync(async () =>
            {
                return await _productRepository.FirstOrDefaultAsync(
                    x => x.Id == productId
                );
            });
        }
        private void Login(Guid userId)
        {
            _currentUser.Id.Returns(userId);
            _currentUser.IsAuthenticated.Returns(true);
        }
    }
}
