using MediatR;
using System;

namespace Tribulus.MarketPlace.Admin.Products.Events;

public class ProductDetailEto : INotification
{
    public Guid Id { get; set; }

    public ProductCompositionDto Product { get; set; }
}
