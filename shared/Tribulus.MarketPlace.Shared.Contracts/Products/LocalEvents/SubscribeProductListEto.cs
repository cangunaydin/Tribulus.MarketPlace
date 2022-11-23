using MediatR;
using System.Collections.Generic;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class SubscribeProductListEto : INotification
{
    public List<ProductAggregateDto> Products { get; set; }
}
