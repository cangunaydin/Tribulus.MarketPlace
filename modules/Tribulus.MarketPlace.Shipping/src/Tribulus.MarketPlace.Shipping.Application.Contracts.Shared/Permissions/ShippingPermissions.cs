using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.Shipping.Permissions
{
    public class ShippingPermissions
    {

        public const string GroupName = "Shipping";

        public static class ProductShippingOptions
        {
            public const string Default = GroupName + ".ProductShippingOptions";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";

        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ShippingPermissions));
        }
    }
}