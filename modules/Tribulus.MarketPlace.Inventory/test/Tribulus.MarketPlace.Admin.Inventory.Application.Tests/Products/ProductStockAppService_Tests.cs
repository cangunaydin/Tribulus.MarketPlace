using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public class ProductStockAppService_Tests : AdminInventoryApplicationTestBase
    {
        private readonly IProductStockAppService _productStockAppService;
        private ICurrentUser _currentUser;
        private readonly InventoryTestData _inventoryTestData;
        private readonly IRepository<ProductStock, Guid> _productStockRepository;

        public ProductStockAppService_Tests()
        {
            _productStockAppService = GetRequiredService<IProductStockAppService>();
            _inventoryTestData = GetRequiredService<InventoryTestData>();
            _productStockRepository = GetRequiredService<IRepository<ProductStock, Guid>>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);

        }
        [Fact]
        public async Task Should_Get_Valid_ProductStock()
        {
            Login(_inventoryTestData.UserJohnId);
            var productStockDto = await _productStockAppService.GetAsync(_inventoryTestData.ProductIphone13Id);
            Assert.NotNull(productStockDto);
            productStockDto.Id.ShouldBe(_inventoryTestData.ProductIphone13Id);
            productStockDto.StockCount.ShouldBe(10);
        }
        [Fact]
        public async Task Should_Get_ProductStocks()
        {
            Login(_inventoryTestData.UserJohnId);
            var productStocksResult = await _productStockAppService.GetListAsync(new ProductStockListFilterDto());
            productStocksResult.Items.Count.ShouldBeGreaterThan(0);
        }
        [Fact]
        public async Task Should_Create_Valid_ProductStock()
        {
            Login(_inventoryTestData.UserJohnId);
            var newProductStock = await _productStockAppService.CreateAsync(new CreateProductStockDto()
            {
                StockCount=10
            });
            var productStock=await GetProductStockOrNullAsync(newProductStock.Id);
            Assert.NotNull(productStock);
            productStock.StockCount.ShouldBe(10);
            
        }
        [Fact]
        public async Task Should_Update_Valid_ProductStock()
        {
            Login(_inventoryTestData.UserJohnId);
            await _productStockAppService
                .UpdateAsync(_inventoryTestData.ProductIphone13Id,new UpdateProductStockDto() { StockCount=5});
           
            var product=await GetProductStockOrNullAsync(_inventoryTestData.ProductIphone13Id);
            product.StockCount.ShouldBe(5);

        }
        private async Task<ProductStock> GetProductStockOrNullAsync(Guid productStockId)
        {
            return await WithUnitOfWorkAsync(async () =>
            {
                return await _productStockRepository.FirstOrDefaultAsync(
                    x => x.Id == productStockId
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
