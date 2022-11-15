using MassTransit;
using System;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace.Extensions;

public class MarketPlaceEndpointNameFormatter :
     IEndpointNameFormatter,ISingletonDependency
{
    static readonly char[] _removeChars = { '.', '+' };
    readonly IEndpointNameFormatter _formatter;
    public string Separator { get; protected set; } = "";

    public MarketPlaceEndpointNameFormatter()
    {
        _formatter = KebabCaseEndpointNameFormatter.Instance;
    }

    public string TemporaryEndpoint(string tag)
    {
        return _formatter.TemporaryEndpoint(tag);
    }

    public string Consumer<T>()
        where T : class, IConsumer
    {
        return _formatter.Consumer<T>();
    }

    public string Message<T>()
        where T : class
    {
        return _formatter.Message<T>();
    }

    public string Saga<T>()
        where T : class, ISaga
    {
        return _formatter.Saga<T>();
    }

    public string ExecuteActivity<T, TArguments>()
        where T : class, IExecuteActivity<TArguments>
        where TArguments : class
    {
        var executeActivity = _formatter.ExecuteActivity<T, TArguments>();

        return SanitizeName(executeActivity);
    }

    public string ExecuteActivity(Type ActivityType)
    {
        return SanitizeName(GetActivityName(ActivityType) + "-execute");
    }
    public string CompensateActivity(Type ActivityType)
    {
        return SanitizeName(GetActivityName(ActivityType)+"-compensate");
    }
    public string GetActivityName(Type activityType)
    {
        const string activity = "Activity";

        var activityName = FormatName(activityType);

        if (activityName.EndsWith(activity, StringComparison.InvariantCultureIgnoreCase))
            activityName = activityName.Substring(0, activityName.Length - activity.Length);

        return SanitizeName(activityName);
    }
    string FormatName(Type type)
    {
        var name =  type.Name;
        if (name.StartsWith('I'))
        {
            name = name.Substring(1);
        }

        return name;
    }
    public string CompensateActivity<T, TLog>()
        where T : class, ICompensateActivity<TLog>
        where TLog : class
    {
        var compensateActivity = _formatter.CompensateActivity<T, TLog>();

        return SanitizeName(compensateActivity);
    }

    public string SanitizeName(string name)
    {
        return _formatter.SanitizeName(name);
    }
}
