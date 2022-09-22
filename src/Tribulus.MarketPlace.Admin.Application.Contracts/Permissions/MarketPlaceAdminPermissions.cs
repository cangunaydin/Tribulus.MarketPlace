namespace Tribulus.MarketPlace.Admin.Permissions;

public static class MarketPlaceAdminPermissions
{
    public const string GroupName = "MarketPlaceAdmin";

    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Orders
    {
        public const string Default = GroupName + ".Orders";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string PlaceOrder = Default + ".PlaceOrder";

    }

    public static class OrderItems
    {
        public const string Default = GroupName + ".OrderItems";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }
}
