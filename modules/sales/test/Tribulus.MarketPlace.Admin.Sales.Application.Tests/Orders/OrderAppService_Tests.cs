using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Products;
using Tribulus.MarketPlace.Sales;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Tribulus.MarketPlace.Sales.Orders;
using NSubstitute;
using Tribulus.MarketPlace.Sales.EntityFrameworkCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Tribulus.MarketPlace.Admin.Sales.Orders
{
    public class OrderAppService_Tests : AdminSalesApplicationTestBase
    {
        private readonly IOrderAppService _orderAppService;
        private ICurrentUser _currentUser;
        private readonly SalesTestData _salesTestData;
        private readonly IOrderRepository _orderRepository;

        public OrderAppService_Tests()
        {
            _orderAppService = GetRequiredService<IOrderAppService>();
            _salesTestData = GetRequiredService<SalesTestData>();
            _orderRepository = GetRequiredService<IOrderRepository>();

        }
        protected override void AfterAddApplication(IServiceCollection services)
        {
            _currentUser = Substitute.For<ICurrentUser>();
            services.AddSingleton(_currentUser);
        }
        public async Task Should_Get_Orders_WithSearch()
        {
            Login(_salesTestData.UserJohnId);
            string searchName = "Order 1";
            var ordersResult = await _orderAppService.GetListAsync(new OrderFilterDto() { Name=searchName});
            ordersResult.Items.Count.ShouldBe(1);
        }
        private void Login(Guid userId)
        {
            _currentUser.Id.Returns(userId);
            _currentUser.IsAuthenticated.Returns(true);
        }
    }
}
