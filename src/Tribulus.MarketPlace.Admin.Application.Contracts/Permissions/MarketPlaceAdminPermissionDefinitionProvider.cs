
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

        //var marketPlaceInventoryAdminGroup = context.AddGroup(MarketPlaceInventoryAdminPermissions.GroupName);
        //var productStockPermissions = marketPlaceInventoryAdminGroup.AddPermission(MarketPlaceAdminPermissions.Products.Default, L("Permission:ProductStocksManagement"));
        //productStockPermissions.AddChild(MarketPlaceAdminPermissions.Products.Create, L("Permission:Create"));
        //productStockPermissions.AddChild(MarketPlaceAdminPermissions.Products.Update, L("Permission:Edit"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MarketPlaceResource>(name);
    }
}
