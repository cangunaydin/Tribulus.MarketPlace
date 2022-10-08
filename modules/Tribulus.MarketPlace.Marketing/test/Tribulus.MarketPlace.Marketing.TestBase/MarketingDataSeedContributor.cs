using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Marketing;

public class MarketingDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IProductRepository _productRepository;
    private readonly MarketingTestData _marketingTestData;
    public MarketingDataSeedContributor(
        IProductRepository productRepository,
        MarketingTestData marketingTestData)
    {
        _productRepository = productRepository;
        _marketingTestData = marketingTestData;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedProductsAsync();

    }
    public async Task SeedProductsAsync()
    {
        //Iphone 13
        var newIphone13 = new Product(
                   _marketingTestData.ProductIphone13Id,
                   _marketingTestData.UserJohnId,
                   _marketingTestData.ProductIphone13Name
                   );
        await _productRepository.InsertAsync(newIphone13);
        //Iphone 13 Pro
        var newIphone13Pro = new Product(
                    _marketingTestData.ProductIphone13ProId,
                    _marketingTestData.UserJohnId,
                    _marketingTestData.ProductIphone13ProName
                    );
        newIphone13Pro.Description = _marketingTestData.ProductIphone13ProDescription;

        await _productRepository.InsertAsync(newIphone13Pro);
        //Iphone 14

        var newIphone14 = new Product(
           _marketingTestData.ProductIphone14Id,
           _marketingTestData.UserJohnId,
           _marketingTestData.ProductIphone14Name
           );
        newIphone14.Description = _marketingTestData.ProductIphone14Description;

        await _productRepository.InsertAsync(newIphone14);
    }
}
