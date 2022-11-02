using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface ProductCreateCompleted : ProductLineCompleted
    {
        ProductTransaction Product { get; }
    }


    public interface ProductLineCompleted :
        FutureCompleted
    {
        Guid ProductTransactionId { get; }
        Guid ProductId { get; }
        string Description { get; }
    }


}
