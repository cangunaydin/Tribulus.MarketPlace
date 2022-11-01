using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface ProductLineCompleted : FutureCompleted
    {
        Guid ProductId { get; }
        Guid ProductLineId { get; }
        string Description { get; }
    }
}
