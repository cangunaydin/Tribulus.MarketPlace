using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public interface IProductDeliveryRepository : IRepository<ProductDelivery, Guid>
    {
    }
}
