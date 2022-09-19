namespace Tribulus.MarketPlace.Permissions;

public static class MarketPlacePermissions
{
    public const string GroupName = "MarketPlace";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Orders
    {
        public const string Default = GroupName + ".Orders";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string PlaceOrder = Default + ".PlaceOrder";

    }
}
