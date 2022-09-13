using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Tribulus.MarketPlace;

public class MarketPlaceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{

    private readonly IRepository<Product, Guid> _productRepository;
    private readonly MarketPlaceTestData _marketPlaceTestData;
    private readonly IIdentityUserRepository _userRepository;

    public MarketPlaceTestDataSeedContributor(
        IRepository<Product, Guid> productRepository,
        MarketPlaceTestData marketPlaceTestData,
        IIdentityUserRepository userRepository)
    {
        _productRepository = productRepository;
        _marketPlaceTestData = marketPlaceTestData;
        _userRepository = userRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */
        await SeedUsers();
        await SeedProductsAsync();
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
        var newIphone13 = new Product(
                   _marketPlaceTestData.ProductIphone13Id,
                   _marketPlaceTestData.UserJohnId,
                   _marketPlaceTestData.ProductIphone13Name,
                   599,
                   10
                   );
        
        await _productRepository.InsertAsync(
              newIphone13
           );

        var newIphone13Pro = new Product(
                    _marketPlaceTestData.ProductIphone13ProId,
                    _marketPlaceTestData.UserJohnId,
                    _marketPlaceTestData.ProductIphone13ProName,
                    699,
                    5
                    );
        newIphone13Pro.Description = _marketPlaceTestData.ProductIphone13ProDescription;

        await _productRepository.InsertAsync(
              newIphone13Pro
           );

        var newIphone14 = new Product(
           _marketPlaceTestData.ProductIphone14Id,
           _marketPlaceTestData.UserJohnId,
           _marketPlaceTestData.ProductIphone14Name,
           799,
           8
           );
        newIphone14.Description = _marketPlaceTestData.ProductIphone14Description;
        await _productRepository.InsertAsync(
              newIphone14
           );

    }
}
