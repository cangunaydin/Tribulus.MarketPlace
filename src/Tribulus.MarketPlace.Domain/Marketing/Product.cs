using System;
using Tribulus.MarketPlace.Products;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Marketing
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public Guid OwnerUserId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; set; }



        private Product() { }

        public Product(Guid id,
            Guid ownerId,
            string name) : base(id)
        {

            OwnerUserId = ownerId;
            UpdateName(name);
        }

        public void UpdateName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name), ProductConsts.MaxNameLength, ProductConsts.MinNameLength);
            Name = name;
        }
    }
}
