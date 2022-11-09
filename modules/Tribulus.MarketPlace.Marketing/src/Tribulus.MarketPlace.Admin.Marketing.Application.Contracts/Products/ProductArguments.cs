using System;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public interface ProductArguments
    {
        public Guid ProductId { get; }
        public string Name { get; }
        public string Description { get; }

        public Guid UserId { get; }

        public int? TenantId { get; }
    }
}
