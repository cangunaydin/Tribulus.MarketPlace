using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.Marketing.Permissions;

public class MarketingPermissions
{
    public const string GroupName = "Marketing";

    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MarketingPermissions));
    }
}
