using MediatR;
using System.Collections.Generic;

namespace Tribulus.MarketPlace.Admin.Products.Events;

public class ProductListSub : INotification
{
    public List<ProductCompositionDto> Products { get; set; }
}
