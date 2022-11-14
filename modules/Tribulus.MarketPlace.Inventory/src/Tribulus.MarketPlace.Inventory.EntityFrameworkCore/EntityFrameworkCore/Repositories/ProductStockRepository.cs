using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Products;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Inventory.EntityFrameworkCore.Repositories
{
    public class ProductStockRepository : EfCoreRepository<IInventoryDbContext, ProductStock, Guid>, IProductStockRepository
    {
        public ProductStockRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
