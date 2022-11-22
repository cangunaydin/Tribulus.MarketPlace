using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.AbpService.Permissions;

public class AbpServicePermissions
{
    public const string GroupName = "AbpService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AbpServicePermissions));
    }
}
