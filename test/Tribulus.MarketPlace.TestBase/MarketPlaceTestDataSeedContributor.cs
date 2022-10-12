using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace;

public class MarketPlaceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly MarketPlaceTestData _marketPlaceTestData;

    public MarketPlaceTestDataSeedContributor(MarketPlaceTestData marketPlaceTestData)
    {

        _marketPlaceTestData = marketPlaceTestData;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */
    }

   
}
