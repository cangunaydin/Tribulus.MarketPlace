using System;

namespace Tribulus.MarketPlace.Products.MassTransit;

public interface Product
{
    public Guid Id { get;  } 
    public string Name { get;  }
    public string Description { get;  }
    public decimal Price { get;  }
    public int StockCount { get;  }
}
