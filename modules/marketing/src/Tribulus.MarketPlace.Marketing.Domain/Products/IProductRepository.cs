using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Marketing.Products
{
    public interface IProductRepository:IRepository<Product,Guid>
    {
    }
}
