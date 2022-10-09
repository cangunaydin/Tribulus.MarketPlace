using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Inventory.Products
{
    public class ProductStockAppService_Tests : InventoryApplicationTestBase
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
        public async Task Should_Get_Products()
        {
            Login(_inventoryTestData.UserJohnId);
            var productStocksResult = await _productStockAppService.GetListAsync(new ProductStockListFilterDto());
            productStocksResult.Items.Count.ShouldBeGreaterThan(0);
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
