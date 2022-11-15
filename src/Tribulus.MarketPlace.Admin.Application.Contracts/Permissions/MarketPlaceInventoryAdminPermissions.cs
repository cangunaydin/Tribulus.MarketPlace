using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Admin.Permissions
{
    public class MarketPlaceInventoryAdminPermissions
    {
        public const string GroupName = "MarketPlaceAdminInventory";

        public static class Products
        {
            public const string Default = GroupName + ".ProductStocks";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";

        }

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
    }
}