using System.Threading.Tasks;

namespace Tribulus.MarketPlace.AggregateService.Products;

public interface IProductAggregateAppService
{

    Task GetProducts();
}
