using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.Sales.Permissions;

public class SalesPermissions
{
    public const string GroupName = "Sales";

    public static class ProductPrices
    {
        public const string Default = GroupName + ".ProductPrices";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }
    public static class Orders
    {
        public const string Default = GroupName + ".Orders";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SalesPermissions));
    }
}
