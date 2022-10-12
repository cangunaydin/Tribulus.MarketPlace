using System;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Shipping.EntityFrameworkCore.Repositories
{
    public class ProductShippingOptionRepository : EfCoreRepository<IShippingDbContext, ProductShippingOptions, Guid>, IProductShippingOptionRepository
    {
        public ProductShippingOptionRepository(IDbContextProvider<IShippingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
