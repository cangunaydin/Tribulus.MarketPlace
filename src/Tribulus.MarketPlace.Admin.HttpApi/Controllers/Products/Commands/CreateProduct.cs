using System;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Commands;

public interface CreateProduct
{
    public Guid ProductId { get; }
    public string Name { get; }
    public string Description { get;  }
    public decimal Price { get;}
    public int StockCount { get;}

    public Guid UserId { get; }
}
