using System;

namespace Tribulus.MarketPlace.Admin.Models
{
    public interface ProductCreateFaulted
    {
        public Guid ProductId { get; }

        public Product Product { get; }

        public string Reason { get; }

    }
}
