using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Sales;

public class SalesDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly SalesTestData _salesTestData;
    private readonly IProductPriceRepository _productPriceRepository;
    private readonly IOrderRepository _orderRepository;

    public SalesDataSeedContributor(
        SalesTestData salesTestData,
        IProductPriceRepository productPriceRepository,
        IOrderRepository orderRepository)
    {
        _salesTestData = salesTestData;
        _productPriceRepository = productPriceRepository;
        _orderRepository = orderRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedProductsAsync();
        await SeedOrdersAsync();

    }

    public async Task SeedOrdersAsync()
    {
        var order1 = new Order(
                    _salesTestData.Order1Id,
                    _salesTestData.UserJohnId,
                    "Order 1"
                );

        order1.AddOrderItem(_salesTestData.Order1ItemId, _salesTestData.ProductIphone13ProId, 1000, 8);
        await _orderRepository.InsertAsync(order1);

    }
    public async Task SeedProductsAsync()
    {

        //Iphone 13
        var newIphone13Price = new ProductPrice(_salesTestData.ProductIphone13Id,
            599);

        await _productPriceRepository.InsertAsync(
             newIphone13Price
          );
        //Iphone 13 Pro

        var newIphone13ProPrice = new ProductPrice(_salesTestData.ProductIphone13ProId,
           699);


        await _productPriceRepository.InsertAsync(
             newIphone13ProPrice
          );

        //Iphone 14
        var newIphone14Price = new ProductPrice(_salesTestData.ProductIphone14Id,
          799);


        await _productPriceRepository.InsertAsync(
           newIphone14Price
        );

    }
}
