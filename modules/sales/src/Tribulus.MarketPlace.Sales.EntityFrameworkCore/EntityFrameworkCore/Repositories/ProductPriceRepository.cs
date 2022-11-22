using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore.Repositories
{
    public class ProductPriceRepository : EfCoreRepository<ISalesDbContext, ProductPrice, Guid>, IProductPriceRepository
    {
        public ProductPriceRepository(IDbContextProvider<ISalesDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

    }
}
