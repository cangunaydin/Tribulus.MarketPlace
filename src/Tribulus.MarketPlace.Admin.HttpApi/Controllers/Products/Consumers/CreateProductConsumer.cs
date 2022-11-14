using MassTransit;
using MassTransit.Courier;
using MassTransit.Futures;
using MediatR;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Controllers.Products.Commands;
using Tribulus.MarketPlace.Extensions;
using Tribulus.MarketPlace.RoutingSlip;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Consumers;

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

    protected override Task BuildItinerary(RoutingSlipBuilder builder, ConsumeContext<CreateProduct> context)
    {
        builder.AddVariable("ProductId", context.Message.ProductId);

        if (context.ExpirationTime.HasValue)
            builder.AddVariable("Deadline", context.ExpirationTime.Value);
       
        _planner.PlanItinerary((FutureConsumeContext<CreateProduct>)context, builder);
        return Task.CompletedTask;

    }
}
