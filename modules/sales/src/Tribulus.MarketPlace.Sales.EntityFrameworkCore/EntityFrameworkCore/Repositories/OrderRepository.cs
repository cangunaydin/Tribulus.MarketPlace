using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Sales.Orders;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Sales.EntityFrameworkCore.Repositories
{
    public class OrderRepository : EfCoreRepository<ISalesDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<ISalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
