using MassTransit.Courier;
using MassTransit.Futures;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Extensions;
using Tribulus.MarketPlace.RoutingSlip;
using Tribulus.MarketPlace.Admin.Controllers.Products.Commands;
using MassTransit.Mediator;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Consumers
{
    public class UpdateProductConsumer : RoutingSlipRequestConsumer<UpdateProduct>
    {
        private readonly IItineraryPlanner<UpdateProduct> _planner;
        private readonly IMediator _mediator;
        public UpdateProductConsumer(
            IItineraryPlanner<UpdateProduct> planner,
            MarketPlaceEndpointNameFormatter formatter,
            IMediator mediator) : base(formatter.Consumer<CreateProductResponseConsumer>())
        {
            _planner = planner;
            _mediator = mediator;
        }

        protected override Task BuildItinerary(RoutingSlipBuilder builder, ConsumeContext<UpdateProduct> context)
        {
            builder.AddVariable("ProductId", context.Message.ProductId);

            if (context.ExpirationTime.HasValue)
                builder.AddVariable("Deadline", context.ExpirationTime.Value);

            _planner.PlanItinerary((FutureConsumeContext<UpdateProduct>)context, builder);
            return Task.CompletedTask;

        }
    }
}