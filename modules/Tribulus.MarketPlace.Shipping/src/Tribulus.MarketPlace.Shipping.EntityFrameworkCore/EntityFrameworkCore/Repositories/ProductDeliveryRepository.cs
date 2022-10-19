using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tribulus.MarketPlace.Shipping.EntityFrameworkCore.Repositories
{
  
    public class ProductDeliveryRepository : EfCoreRepository<IShippingDbContext, ProductDelivery, Guid>, IProductDeliveryRepository
    {
        public ProductDeliveryRepository(IDbContextProvider<IShippingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
