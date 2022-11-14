using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.Inventory.Permissions;

public class InventoryPermissions
{
    public const string GroupName = "Inventory";

    public static class ProductStocks
    {
        public const string Default = GroupName + ".ProductStocks";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InventoryPermissions));
    }
}
