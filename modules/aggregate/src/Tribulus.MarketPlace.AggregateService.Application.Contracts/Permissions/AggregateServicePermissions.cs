using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.AggregateService.Permissions;

public class AggregateServicePermissions
{
    public const string GroupName = "AggregateService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AggregateServicePermissions));
    }
}
