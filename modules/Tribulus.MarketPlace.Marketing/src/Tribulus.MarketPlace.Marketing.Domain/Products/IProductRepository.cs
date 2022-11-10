using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Marketing.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
