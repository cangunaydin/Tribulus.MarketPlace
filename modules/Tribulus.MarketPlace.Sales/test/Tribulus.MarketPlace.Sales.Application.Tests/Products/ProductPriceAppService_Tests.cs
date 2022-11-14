using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Sales.Products
{
    public class ProductPriceAppService_Tests : SalesApplicationTestBase
    {
        private readonly IProductPriceAppService _productPriceAppService;
        private ICurrentUser _currentUser;
        private readonly SalesTestData _salesTestData;
        private readonly IRepository<ProductPrice, Guid> _productPriceRepository;

        public ProductPriceAppService_Tests()
        {
            _productPriceAppService = GetRequiredService<IProductPriceAppService>();
            _currentUser = GetRequiredService<ICurrentUser>();
            _salesTestData = GetRequiredService<SalesTestData>();
            _productPriceRepository = GetRequiredService<IRepository<ProductPrice, Guid>>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);
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
