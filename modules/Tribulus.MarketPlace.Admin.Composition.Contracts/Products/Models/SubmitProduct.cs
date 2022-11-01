using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface SubmitProduct
    {
        public Guid ProductId { get; }
        public Guid CorrelationId { get; }

        //Marketing
        public string Name { get; set; }
        public string Description { get; set; }

        //Sales
        public decimal Price { get; set; }

        //Inventory
        public int StockCount { get; set; }
    }
}
