using MediatR;
using System;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class GetProductDetailEto : INotification
{
    public Guid Id { get; set; }

    public ProductAggregateDto Product { get; set; }
}
