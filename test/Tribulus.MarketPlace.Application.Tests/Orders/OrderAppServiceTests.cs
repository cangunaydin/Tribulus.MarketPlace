using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Orders;
using Tribulus.MarketPlace.Products;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Orders;

public class OrderAppServiceTests : MarketPlaceApplicationTestBase
{
    private readonly IOrderAppService _orderAppService;
    private ICurrentUser _currentUser;
    private readonly MarketPlaceTestData _marketPlaceTestData;
    private readonly IRepository<Order, Guid> _orderRepository;

    public OrderAppServiceTests()
    {
        _orderAppService = GetRequiredService<IOrderAppService>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _marketPlaceTestData = GetRequiredService<MarketPlaceTestData>();
        _orderRepository = GetRequiredService<IRepository<Order, Guid>>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
    [Fact]
    public async Task Should_Create_New_Valid_Order()
    {
        Login(_marketPlaceTestData.UserAdminId);

        var orderDto = await _orderAppService.CreateAsync(new CreateOrderDto()
        {            
            Name = "New Order",
           
        });

        orderDto.Name.ShouldBe("New Order");
    }
    [Fact]
    public async Task Should_Update_Order()
    {
        Login(_marketPlaceTestData.UserAdminId);
        await _orderAppService.UpdateAsync(_marketPlaceTestData.Order1Id, new UpdateOrderDto()
        {
            Name = "Updated Order Name",
          
        });
        var updatedProduct = await GetOrderOrNullAsync(_marketPlaceTestData.Order1Id);
        updatedProduct.Name.ShouldBe("Updated Order Name");
    
    }
    
    [Fact]
    public async Task Should_Filter_Orders_By_Name()
    {
        Login(_marketPlaceTestData.UserAdminId);
        string searchName = "New";
        var ordersResult = await _orderAppService.GetOrdersAsync(new OrderFilterDto() { Name = searchName });
        foreach (var order in ordersResult.Items)
        {
            order.Name.ShouldContain(searchName);
        }
    }

    [Fact]
    public async Task Should_add_order_items()
    {
        Login(_marketPlaceTestData.UserAdminId);
        var orderitem = await _orderAppService.CreateOrderItemAsync(_marketPlaceTestData.Order1Id, new CreateOrderItemDto()
        {
            ProductId = _marketPlaceTestData.ProductIphone13Id,
            Price = 100,
            Quantity = 1
        });

        orderitem.ShouldNotBeNull();
        orderitem.ProductId.ShouldBe(_marketPlaceTestData.ProductIphone13Id);

    }

    [Fact]
    public async Task Should_place_order()
    {
        Login(_marketPlaceTestData.UserAdminId);
        await _orderAppService.PlaceOrder(_marketPlaceTestData.Order1Id);
       
    }

    [Fact]
    public async Task Should_Not_Place_Order_With_High_Product_StockCount()
    {
        var orderitem = await _orderAppService.CreateOrderItemAsync(_marketPlaceTestData.Order1Id, new CreateOrderItemDto()
        {
            ProductId = _marketPlaceTestData.ProductIphone13Id,
            Price = 100,
            Quantity = 10
        });

        var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
        {
            await _orderAppService.PlaceOrder(_marketPlaceTestData.Order1Id);
        });
    }

   

    private async Task<Order> GetOrderOrNullAsync(Guid orderId)
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            return await _orderRepository.FirstOrDefaultAsync(
                x => x.Id == orderId
            );
        });
    }
    private void Login(Guid userId)
    {
        _currentUser.Id.Returns(userId);
        _currentUser.IsAuthenticated.Returns(true);
    }


}
