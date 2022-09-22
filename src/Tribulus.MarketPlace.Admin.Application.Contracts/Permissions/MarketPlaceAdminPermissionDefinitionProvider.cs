
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Admin.Permissions;

public class MarketPlaceAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var marketPlaceAdminGroup = context.AddGroup(MarketPlaceAdminPermissions.GroupName);

        var productPermissions = marketPlaceAdminGroup.AddPermission(MarketPlaceAdminPermissions.Products.Default, L("Permission:ProductsManagement"));
        productPermissions.AddChild(MarketPlaceAdminPermissions.Products.Create, L("Permission:Create"));
        productPermissions.AddChild(MarketPlaceAdminPermissions.Products.Update, L("Permission:Edit"));

        //Define your own permissions here. Example:
        var orderPermissions = marketPlaceAdminGroup.AddPermission(MarketPlaceAdminPermissions.Orders.Default, L("Permission:OrdersManagement"));
        orderPermissions.AddChild(MarketPlaceAdminPermissions.Orders.Create, L("Permission:Create"));
        orderPermissions.AddChild(MarketPlaceAdminPermissions.Orders.Update, L("Permission:Update"));
        orderPermissions.AddChild(MarketPlaceAdminPermissions.Orders.PlaceOrder, L("Permission:PlaceOrder"));

        var orderItemPermissions = marketPlaceAdminGroup.AddPermission(MarketPlaceAdminPermissions.OrderItems.Default, L("Permission:OrdersItemsManagement"));
        orderItemPermissions.AddChild(MarketPlaceAdminPermissions.OrderItems.Create, L("Permission:Create"));
        orderItemPermissions.AddChild(MarketPlaceAdminPermissions.OrderItems.Update, L("Permission:Update"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MarketPlaceResource>(name);
    }
}
