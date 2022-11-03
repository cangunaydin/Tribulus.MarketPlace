using MassTransit;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Models;

namespace Tribulus.MarketPlace.Admin.Components.Consumers
{
    public class SubmitProductConsumer : RoutingSlipRequestConsumer<SubmitProduct>
    {
        private readonly IItineraryPlanner<Product> _planner;
        public SubmitProductConsumer(IItineraryPlanner<Product> planner, IEndpointNameFormatter formatter) : base(formatter.Consumer<SubmitProductResponseConsumer>())
        {
            _planner = planner;
        }

        protected override Task BuildItinerary(RoutingSlipBuilder builder, ConsumeContext<SubmitProduct> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
