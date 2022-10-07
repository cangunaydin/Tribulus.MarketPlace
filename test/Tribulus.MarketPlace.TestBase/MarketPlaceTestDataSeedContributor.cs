using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Sales;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Tribulus.MarketPlace;

public class MarketPlaceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{

    private readonly IRepository<Product, Guid> _productRepository;
    private readonly IRepository<ProductPrice, Guid> _productPriceRepository;
    private readonly IRepository<ProductStock, Guid> _productStockRepository;
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly IRepository<OrderItemQuantity, Guid> _orderItemQuantityRepository;
    private readonly MarketPlaceTestData _marketPlaceTestData;
    private readonly IIdentityUserRepository _userRepository;

    public MarketPlaceTestDataSeedContributor(
        IRepository<Product, Guid> productRepository,
        IRepository<ProductPrice, Guid> productPriceRepository,
        IRepository<ProductStock, Guid> productStockRepository,
        MarketPlaceTestData marketPlaceTestData,
        IIdentityUserRepository userRepository,
        IRepository<Order, Guid> orderRepository,
        IRepository<OrderItemQuantity, Guid> orderItemQuantityRepository)
    {
        _productRepository = productRepository;
        _productPriceRepository = productPriceRepository;
        _productStockRepository = productStockRepository;

        _marketPlaceTestData = marketPlaceTestData;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _orderItemQuantityRepository = orderItemQuantityRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */
        await SeedUsers();
        await SeedProductsAsync();
        await SeedOrdersAsync();
    }

    public async Task SeedOrdersAsync()
    {
        var order1 = new Order(
                    _marketPlaceTestData.Order1Id,
                    _marketPlaceTestData.UserJohnId,
                    "Order 1"
                );

        order1.AddOrderItem(_marketPlaceTestData.Order1ItemId, _marketPlaceTestData.ProductIphone13ProId, 1000, 8);
        await _orderRepository.InsertAsync(order1);

    }

    public async Task SeedUsers()
    {
        var adminUser = await _userRepository.FindByNormalizedUserNameAsync("ADMIN");
        _marketPlaceTestData.UserAdminId = adminUser.Id;

        await _userRepository.InsertAsync(
            new IdentityUser(
                _marketPlaceTestData.UserJohnId,
                _marketPlaceTestData.UserJohnUserName,
                "cangunaydin@gmail.com"
            )
        );
    }
    public async Task SeedProductsAsync()
    {

        //Iphone 13
        var newIphone13 = new Product(
                   _marketPlaceTestData.ProductIphone13Id,
                   _marketPlaceTestData.UserJohnId,
                   _marketPlaceTestData.ProductIphone13Name
                   );
        var newIphone13Price = new ProductPrice(_marketPlaceTestData.ProductIphone13Id,
            599);

        var newIphone13Stock = new ProductStock(_marketPlaceTestData.ProductIphone13Id,
            10);


        await _productRepository.InsertAsync(
              newIphone13
           );

        await _productPriceRepository.InsertAsync(
             newIphone13Price
          );

        await _productStockRepository.InsertAsync(
     newIphone13Stock
  );
        //Iphone 13 Pro
        var newIphone13Pro = new Product(
                    _marketPlaceTestData.ProductIphone13ProId,
                    _marketPlaceTestData.UserJohnId,
                    _marketPlaceTestData.ProductIphone13ProName
                    );
        newIphone13Pro.Description = _marketPlaceTestData.ProductIphone13ProDescription;

        var newIphone13ProPrice = new ProductPrice(_marketPlaceTestData.ProductIphone13ProId,
           699);

        var newIphone13ProStock = new ProductStock(_marketPlaceTestData.ProductIphone13ProId,
            5);

        await _productRepository.InsertAsync(
      newIphone13Pro
   );

        await _productPriceRepository.InsertAsync(
             newIphone13ProPrice
          );

        await _productStockRepository.InsertAsync(
     newIphone13ProStock
  );
        //Iphone 14

        var newIphone14 = new Product(
           _marketPlaceTestData.ProductIphone14Id,
           _marketPlaceTestData.UserJohnId,
           _marketPlaceTestData.ProductIphone14Name
           );
        newIphone14.Description = _marketPlaceTestData.ProductIphone14Description;

        var newIphone14Price = new ProductPrice(_marketPlaceTestData.ProductIphone14Id,
          799);

        var newIphone14Stock = new ProductStock(_marketPlaceTestData.ProductIphone14Id,
            8);
           


        await _productRepository.InsertAsync(
              newIphone14
           );

        await _productPriceRepository.InsertAsync(
           newIphone14Price
        );

        await _productStockRepository.InsertAsync(
     newIphone14Stock
  );

    }
}
