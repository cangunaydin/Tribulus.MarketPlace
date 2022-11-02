using System;

namespace Tribulus.MarketPlace.Admin.Products
{
    //Order--> ProductTransaction
    public class ProductTransaction
    {
        public Guid ProductId { get; set; }
        
        public Product[] Products { get; set; }
    }
}
