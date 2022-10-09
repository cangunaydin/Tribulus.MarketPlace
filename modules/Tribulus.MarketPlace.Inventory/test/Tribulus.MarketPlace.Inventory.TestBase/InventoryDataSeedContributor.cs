using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Orders;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Tribulus.MarketPlace.Inventory;

public class InventoryDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly InventoryTestData _inventoryTestData;
    private readonly IProductStockRepository _productStockRepository;
    private readonly IOrderItemQuantityRepository _orderItemQuantityRepository;

    public InventoryDataSeedContributor(
        InventoryTestData inventoryTestData, 
        IProductStockRepository productStockRepository,
        IOrderItemQuantityRepository orderItemQuantityRepository)
    {
        _inventoryTestData = inventoryTestData;
        _productStockRepository = productStockRepository;
        _orderItemQuantityRepository = orderItemQuantityRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedProductsAsync();
        await SeedOrderItemQuantities();
    }

    private async Task SeedOrderItemQuantities()
    {
        var orderItemQuantity = new OrderItemQuantity(_inventoryTestData.OrderItemId, 
            _inventoryTestData.OrderId, 
            _inventoryTestData.ProductIphone13Id, 
            3);
        await _orderItemQuantityRepository.InsertAsync(orderItemQuantity);

        var orderItemQuantity2 = new OrderItemQuantity(_inventoryTestData.OrderItem2Id,
            _inventoryTestData.OrderId,
            _inventoryTestData.ProductIphone14Id,
            6);
        await _orderItemQuantityRepository.InsertAsync(orderItemQuantity2);
    }

    public async Task SeedProductsAsync()
    {

        //Iphone 13
        var newIphone13Stock = new ProductStock(_inventoryTestData.ProductIphone13Id,10);
        await _productStockRepository.InsertAsync(newIphone13Stock);
        //Iphone 13 Pro
        var newIphone13ProStock = new ProductStock(_inventoryTestData.ProductIphone13ProId,5);
        await _productStockRepository.InsertAsync( newIphone13ProStock);
        //Iphone 14
        var newIphone14Stock = new ProductStock(_inventoryTestData.ProductIphone14Id,8);
        await _productStockRepository.InsertAsync(newIphone14Stock);

    }
}
