using System;

namespace Tribulus.MarketPlace.Admin.Products.Events
{
    public interface SubmitProductEvent
    {
        public Guid ProductId { get; }
        public Guid CorrelationId { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}
