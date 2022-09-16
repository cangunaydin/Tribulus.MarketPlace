using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderManager_Tests : MarketPlaceDomainTestBase
    {
        private readonly OrderManager _orderManager;
        private readonly MarketPlaceTestData _marketPlaceTestData;
        private readonly IRepository<Order, Guid> _orderRepository;
        public OrderManager_Tests()
        {
            _orderManager=GetRequiredService<OrderManager>();
            _marketPlaceTestData=GetRequiredService<MarketPlaceTestData>();
            _orderRepository = GetRequiredService<IRepository<Order, Guid>>();
        }
        [Fact]
        public async Task Should_Not_Place_Order_Without_Product_StockCount()
        {
            var order1 = await _orderRepository.GetAsync(_marketPlaceTestData.Order1Id);
            //var order1 = new Order(
            //       _marketPlaceTestData.Order1Id,
            //       _marketPlaceTestData.Order1OwnerUserId,
            //       "Order 1"
            //   );

            //order1.AddOrderItem(_marketPlaceTestData.Order1ItemId, _marketPlaceTestData.ProductIphone13ProId, 1000, 10);

            var exception=await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _orderManager.PlaceOrderAsync(order1);
            });
        }
    }
}
