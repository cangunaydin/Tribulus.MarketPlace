using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public class ProductPriceAppService_Tests : AdminSalesApplicationTestBase
    {
        private readonly IProductPriceAppService _productPriceAppService;
        private ICurrentUser _currentUser;
        private readonly SalesTestData _salesTestData;
        private readonly IRepository<ProductPrice, Guid> _productPriceRepository;

        public ProductPriceAppService_Tests()
        {
            _productPriceAppService = GetRequiredService<IProductPriceAppService>();
            _salesTestData = GetRequiredService<SalesTestData>();
            _productPriceRepository = GetRequiredService<IRepository<ProductPrice, Guid>>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);
        }
        [Fact]
        public async Task Should_Create_New_Valid_Product()
        {
            Login(_salesTestData.UserJohnId);

            var productDto = await _productPriceAppService.CreateAsync(new CreateProductPriceDto()
            {
               Price=100
            });
            productDto.Price.ShouldBe(100);
        }
        [Fact]
        public async Task Should_Update_Product()
        {
            Login(_salesTestData.UserJohnId);
            await _productPriceAppService.UpdateAsync(_salesTestData.ProductIphone13Id, new UpdateProductPriceDto()
            {
                Price=30
            });
            var updatedProduct = await GetProductOrNullAsync(_salesTestData.ProductIphone13Id);
            updatedProduct.Price.ShouldBe(30);
        }
        [Fact]
        public async Task Should_Get_Valid_Product()
        {
            Login(_salesTestData.UserJohnId);
            var product = await _productPriceAppService.GetAsync(_salesTestData.ProductIphone13Id);
            product.ShouldNotBeNull();

        }

        [Fact]
        public async Task Should_Get_Products()
        {
            Login(_salesTestData.UserJohnId);
            string searchName = "iphone";
            var productsResult = await _productPriceAppService.GetListAsync(new ProductPriceListFilterDto());
            productsResult.Items.Count.ShouldBe(3);
        }
        private async Task<ProductPrice> GetProductOrNullAsync(Guid productPriceId)
        {
            return await WithUnitOfWorkAsync(async () =>
            {
                return await _productPriceRepository.FirstOrDefaultAsync(
                    x => x.Id == productPriceId
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
