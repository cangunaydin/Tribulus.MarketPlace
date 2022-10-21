using System;

namespace Tribulus.MarketPlace.Admin.Products
{
    public interface FullfillMarketingProductMessage
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price{ get; set; }
        public int StockCount{ get; set; }
    }
}
