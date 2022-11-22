using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Sales.Products
{
    public class ProductPrice : FullAuditedAggregateRoot<Guid>
    {
        public decimal Price { get; private set; }

        private ProductPrice() { }

        public ProductPrice(Guid id,
            decimal price) : base(id)
        {
            UpdatePrice(price);
        }
        public void UpdatePrice(decimal price)
        {
            Check.Positive(price, nameof(price));
            Price = price;
        }


    }
}
