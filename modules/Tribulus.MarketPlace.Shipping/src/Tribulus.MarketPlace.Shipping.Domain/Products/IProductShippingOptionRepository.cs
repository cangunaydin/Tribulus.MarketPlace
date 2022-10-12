using System;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public interface IProductShippingOptionRepository : IRepository<ProductShippingOptions, Guid>
    {
    }
}
