using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface SubmitProductTransactionFaulted : FutureFaulted
    {
        public Guid ProductId { get; set; }
    }
}

