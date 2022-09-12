using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Data;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.EntityFrameworkCore;

public class EntityFrameworkCoreMarketPlaceDbSchemaMigrator
    : IMarketPlaceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMarketPlaceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MarketPlaceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MarketPlaceDbContext>()
            .Database
            .MigrateAsync();
    }
}
