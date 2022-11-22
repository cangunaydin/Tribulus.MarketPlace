using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public interface ProductPriceArguments
    {
        public Guid ProductId { get; }
        public decimal Price { get; }

        public Guid UserId { get; }

        public int? TenantId { get; }
    }
}
