using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface SubmitProductCompleted 
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}

