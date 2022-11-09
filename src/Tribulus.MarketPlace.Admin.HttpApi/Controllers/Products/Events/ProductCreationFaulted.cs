using System;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Events;

public interface ProductCreationFaulted
{
    public Guid ProductId { get; }

    public string Reason { get; }

}

