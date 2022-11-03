using System;

namespace Tribulus.MarketPlace.Admin.Products.Messages
{
    public interface MarketingProductArgument
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
