using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Sales.Products
{
    public interface IProductPriceRepository:IRepository<ProductPrice,Guid>
    {
    }
}
