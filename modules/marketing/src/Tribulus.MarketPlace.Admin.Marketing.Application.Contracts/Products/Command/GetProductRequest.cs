using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Admin.Marketing.Products.Command
{
    public interface GetProductRequest
    {
        public Guid ProductId { get; }

    }
}
