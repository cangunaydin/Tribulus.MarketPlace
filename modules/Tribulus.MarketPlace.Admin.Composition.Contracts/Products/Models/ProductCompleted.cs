using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface ProductCompleted : FutureCompleted
    {
        public Guid ProductId { get; set; }
    }
}

