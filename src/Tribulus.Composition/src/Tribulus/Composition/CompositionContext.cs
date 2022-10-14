using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using System;
using System.Linq;

namespace Tribulus.Composition;

public class CompositionContext : ICompositionContext, IScopedDependency
{
    public HttpRequest HttpRequest { get; set; }

    public string RequestId { get; set; }

    readonly ConcurrentDictionary<Type, List<CompositionEventHandler<object>>> _compositionEventsSubscriptions = new();

    public CompositionContext()
    {

    }
    public Task RaiseEvent(object @event)
    {
        if (_compositionEventsSubscriptions.TryGetValue(@event.GetType(), out var compositionHandlers))
        {
            var tasks = compositionHandlers.Select(handler => handler.Invoke(@event)).ToList();

            return Task.WhenAll(tasks);
        }

        return Task.CompletedTask;
    }


    public void Subscribe<TEvent>(CompositionEventHandler<TEvent> handler)
    {
        if (!_compositionEventsSubscriptions.TryGetValue(typeof(TEvent), out var handlers))
        {
            handlers = new List<CompositionEventHandler<object>>();
            _compositionEventsSubscriptions.TryAdd(typeof(TEvent), handlers);
        }
        handlers.Add((@event) => handler((TEvent) @event));
    }

    public void CleanupSubscribers()
    {
        _compositionEventsSubscriptions.Clear();
    }
}