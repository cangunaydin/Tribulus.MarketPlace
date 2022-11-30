using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Admin.Inventory.Products.Command
{
    public interface GetProductStockRequest
    {
        public Guid ProductId { get; }

    }
}
