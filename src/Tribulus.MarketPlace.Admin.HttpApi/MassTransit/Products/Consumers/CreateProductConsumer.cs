﻿using MassTransit;
using MassTransit.Courier;
using MediatR;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Extensions;
using Tribulus.MarketPlace.Products.LocalEvents;
using Tribulus.MarketPlace.Products.MassTransit.Commands;
using Tribulus.MarketPlace.RoutingSlip;

namespace Tribulus.MarketPlace.Admin.MassTransit.Products.Consumers;

public class CreateProductConsumer : RoutingSlipRequestConsumer<CreateProduct>
{
    private readonly IItineraryPlanner<CreateProduct> _planner;
    private readonly IMediator _mediator;
    public CreateProductConsumer(
        IItineraryPlanner<CreateProduct> planner, 
        MarketPlaceEndpointNameFormatter formatter, 
        IMediator mediator) : base(formatter.Consumer<CreateProductResponseConsumer>())
    {
        _planner = planner;
        _mediator = mediator;
    }

    protected override async Task BuildItinerary(RoutingSlipBuilder builder, ConsumeContext<CreateProduct> context)
    {
        builder.AddVariable("ProductId", context.Message.ProductId);

        if (context.ExpirationTime.HasValue)
            builder.AddVariable("Deadline", context.ExpirationTime.Value);
       
        _planner.ProduceItinerary(context.Message, builder);

    }
}