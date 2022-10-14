using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tribulus.Composition;


public interface ICompositionContext
{
    HttpRequest HttpRequest { get; set; }

    string RequestId { get; set; }
    Task RaiseEvent(object @event);

    void Subscribe<TEvent>(CompositionEventHandler<TEvent> handler);

    void CleanupSubscribers();

}