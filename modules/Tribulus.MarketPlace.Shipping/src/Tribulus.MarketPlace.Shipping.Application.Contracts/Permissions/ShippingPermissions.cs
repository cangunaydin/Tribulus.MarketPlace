using Volo.Abp.Reflection;

namespace Tribulus.MarketPlace.Shipping.Permissions;

public class ShippingPermissions
{
    public const string GroupName = "Shipping";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ShippingPermissions));
    }
}
