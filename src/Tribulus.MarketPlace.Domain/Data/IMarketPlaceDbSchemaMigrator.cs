using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Data;

public interface IMarketPlaceDbSchemaMigrator
{
    Task MigrateAsync();
}
