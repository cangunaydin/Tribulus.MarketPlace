using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Tribulus.Composition;

public class CompositionRouteRegistry
{
    public string Route { get; set; }

    public HttpMethod Method { get; set; } 

    public Type? ViewModelType { get; set; }

    public List<Type?> InputTypes { get; set; }

    public List<CompositionRouteItem> Handlers { get; set; }

    public List<CompositionRouteItem> Subscribers { get; set; }
}
