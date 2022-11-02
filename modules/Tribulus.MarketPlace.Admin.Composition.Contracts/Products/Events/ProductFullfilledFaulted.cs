using System;

namespace Tribulus.MarketPlace.Admin.Products
{
    public interface ProductFullfilledFaulted 
    {
        public Guid ProductId { get; set; }

        public string Reason { get; set; }
    }
}

