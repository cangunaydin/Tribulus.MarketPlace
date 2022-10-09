using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Sales;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

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
