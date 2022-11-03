using System;

namespace Tribulus.MarketPlace.Admin.Models
{
    public interface SubmitProductFaulted 
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Reason { get; set; }
    }
}

