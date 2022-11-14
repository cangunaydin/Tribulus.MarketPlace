using MediatR;
using System.Collections.Generic;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class ProductListSub : INotification
{
    public List<ProductCompositionDto> Products { get; set; }
}
