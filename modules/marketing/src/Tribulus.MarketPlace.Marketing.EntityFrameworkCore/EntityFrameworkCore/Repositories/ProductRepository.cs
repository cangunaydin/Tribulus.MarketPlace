using System;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Marketing.EntityFrameworkCore.Repositories
{
    public class ProductRepository : EfCoreRepository<MarketingDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<MarketingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
