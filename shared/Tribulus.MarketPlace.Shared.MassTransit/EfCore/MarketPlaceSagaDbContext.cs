using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Tribulus.MarketPlace.EfCore;

public class MarketPlaceSagaDbContext :
SagaDbContext
{
    public MarketPlaceSagaDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new FutureStateMap(); }
    }
}
