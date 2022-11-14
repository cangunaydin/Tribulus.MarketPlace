using MediatR;
using System;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class ProductDetailEto : INotification
{
    public Guid Id { get; set; }

    public ProductCompositionDto Product { get; set; }
}
