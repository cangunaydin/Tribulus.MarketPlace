using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Admin.Sales.Products.Command
{
    public interface GetProductPriceRequest
    {
        public Guid ProductId { get; }

    }
}
