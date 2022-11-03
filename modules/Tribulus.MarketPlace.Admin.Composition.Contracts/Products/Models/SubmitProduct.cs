using System;

namespace Tribulus.MarketPlace.Admin.Models
{
    public interface SubmitProduct
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
