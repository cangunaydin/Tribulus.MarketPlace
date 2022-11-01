using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface ProductFaulted : FutureFaulted
    {
        public Guid ProductId { get; set; }
    }
}

