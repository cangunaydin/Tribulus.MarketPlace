using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Data;

/* This is used if database provider does't define
 * IMarketPlaceDbSchemaMigrator implementation.
 */
public class NullMarketPlaceDbSchemaMigrator : IMarketPlaceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
