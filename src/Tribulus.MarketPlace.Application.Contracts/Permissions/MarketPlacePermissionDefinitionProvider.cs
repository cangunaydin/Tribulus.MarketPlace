using Tribulus.MarketPlace.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Permissions;

public class MarketPlacePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var marketPlaceGroup = context.AddGroup(MarketPlacePermissions.GroupName);
        //Define your own permissions here. Example:
        var orderPermissions=marketPlaceGroup.AddPermission(MarketPlacePermissions.Orders.Default, L("Permission:OrdersManagement"));
        orderPermissions.AddChild(MarketPlacePermissions.Orders.Create, L("Permission:Create"));
        orderPermissions.AddChild(MarketPlacePermissions.Orders.Update, L("Permission:Update"));
        orderPermissions.AddChild(MarketPlacePermissions.Orders.PlaceOrder, L("Permission:PlaceOrder"));

        var orderItemPermissions = marketPlaceGroup.AddPermission(MarketPlacePermissions.OrderItems.Default, L("Permission:OrdersItemsManagement"));
        orderItemPermissions.AddChild(MarketPlacePermissions.OrderItems.Create, L("Permission:Create"));
        orderItemPermissions.AddChild(MarketPlacePermissions.OrderItems.Update, L("Permission:Update"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MarketPlaceResource>(name);
    }
}
