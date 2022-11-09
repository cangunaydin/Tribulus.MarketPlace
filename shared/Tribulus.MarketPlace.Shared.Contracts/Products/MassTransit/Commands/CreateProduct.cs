using System;

namespace Tribulus.MarketPlace.Products.MassTransit.Commands;

public interface CreateProduct
{
    public Guid ProductId { get; }
    public string Name { get; }
    public string Description { get;  }
    public decimal Price { get;}
    public int StockCount { get;}
}
