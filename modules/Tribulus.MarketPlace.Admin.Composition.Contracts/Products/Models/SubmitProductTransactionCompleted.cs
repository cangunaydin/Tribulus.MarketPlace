using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface SubmitProductTransactionCompleted : FutureCompleted
    {
        public Guid ProductId { get; set; }
    }
}

