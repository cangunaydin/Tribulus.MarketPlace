using MassTransit;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Admin.Components.ItineraryPlanners
{
    public interface IRoutingSlipItineraryPlanner<in TInput> where TInput : class
    {
        Task PlanItinerary(TInput value, IItineraryBuilder builder);
    }
}
