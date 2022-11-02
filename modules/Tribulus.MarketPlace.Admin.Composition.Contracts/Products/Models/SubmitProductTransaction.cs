using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface SubmitProductTransaction
    {
        public Guid ProductTransactionId { get; }

        public Product[] Products{ get; set; }
    }
}
