using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tribulus.Composition;

public class CompositionRouteItem
{
    public string Route { get; set; }

    public string HttpMethod { get; set; }

    public Type ComponentType { get; set; }

    public MethodInfo MethodInfo { get; set; }

    public List<Type?> InputTypes { get; set; }

    public Type? ViewModelType { get; set; }

    public bool IsSubscriber
    {
        get
        {
            return ComponentType.IsAssignableTo(typeof(ICompositionSubscribeService));
        }
    }

}
