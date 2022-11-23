using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.AggregateService.Products.Commands;

public interface CreateProduct
{
    public Guid ProductId { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int StockCount { get; }

    public Guid UserId { get; }
}
