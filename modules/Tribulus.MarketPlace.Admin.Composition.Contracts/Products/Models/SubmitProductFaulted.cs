using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface SubmitProductFaulted : FutureFaulted
    {
        public Guid ProductId { get; set; }
    }
}

