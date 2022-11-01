using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    [ExcludeFromTopology]
    public interface ProductLine
    {
        Guid ProductId { get; }

        Guid ProductLineId { get; }
    }
}
