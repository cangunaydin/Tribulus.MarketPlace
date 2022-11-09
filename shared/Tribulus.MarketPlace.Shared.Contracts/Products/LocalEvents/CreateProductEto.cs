using MassTransit.Courier;
using MediatR;
using Tribulus.MarketPlace.Products.MassTransit.Commands;

namespace Tribulus.MarketPlace.Products.LocalEvents;

public class CreateProductEto : INotification
{
    public CreateProduct Product { get; set; }

    public ItineraryBuilder builder { get; set; }
}
