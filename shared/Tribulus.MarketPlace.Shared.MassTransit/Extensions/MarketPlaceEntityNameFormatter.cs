
using MassTransit;

namespace Tribulus.MarketPlace.Extensions;

public class MarketPlaceEntityNameFormatter :
 IEntityNameFormatter
{
    readonly IEntityNameFormatter _entityNameFormatter;

    public MarketPlaceEntityNameFormatter(IEntityNameFormatter entityNameFormatter)
    {
        _entityNameFormatter = entityNameFormatter;
    }

    public string FormatEntityName<T>()
    {
        //if (typeof(T).ClosesType(typeof(Link<>), out Type[] types)
        //    || typeof(T).ClosesType(typeof(Up<>), out types)
        //    || typeof(T).ClosesType(typeof(Down<>), out types)
        //    || typeof(T).ClosesType(typeof(Unlink<>), out types))
        //{
        //    var name = (string)typeof(IEntityNameFormatter)
        //        .GetMethod("FormatEntityName")
        //        .MakeGenericMethod(types)
        //        .Invoke(_entityNameFormatter, Array.Empty<object>());

        //    var suffix = typeof(T).Name.Split('`').First();

        //    return $"{name}-{suffix}";
        //}

        return _entityNameFormatter.FormatEntityName<T>();
    }
}
