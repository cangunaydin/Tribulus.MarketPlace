using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface ProductTransactionProduct : ProductTransactionLine
    {
        Product Product { get; set; }
    }

    [ExcludeFromTopology]
    public interface ProductTransactionLine
    {
        Guid ProductTransactionId { get; set; }
        Guid ProductId { get; set; }

    }
}
