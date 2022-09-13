
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Admin.Permissions;

public class MarketPlaceAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var marketPlaceAdminGroup = context.AddGroup(MarketPlaceAdminPermissions.GroupName);

        var productsPermission = marketPlaceAdminGroup.AddPermission(MarketPlaceAdminPermissions.Products.Default, L("Permission:ProductsManagement"));
        productsPermission.AddChild(MarketPlaceAdminPermissions.Products.Create, L("Permission:Create"));
        productsPermission.AddChild(MarketPlaceAdminPermissions.Products.Update, L("Permission:Edit"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(AdminPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MarketPlaceResource>(name);
    }
}
