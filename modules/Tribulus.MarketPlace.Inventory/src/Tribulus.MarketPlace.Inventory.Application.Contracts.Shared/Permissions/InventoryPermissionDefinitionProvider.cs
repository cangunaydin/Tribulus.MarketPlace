using Tribulus.MarketPlace.Inventory.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Inventory.Permissions;

public class InventoryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var inventoryGroup = context.AddGroup(InventoryPermissions.GroupName);


        var productPermissions = inventoryGroup.AddPermission(InventoryPermissions.ProductStocks.Default, L("Permission:ProductStocksManagement"));
        productPermissions.AddChild(InventoryPermissions.ProductStocks.Create, L("Permission:Create"));
        productPermissions.AddChild(InventoryPermissions.ProductStocks.Update, L("Permission:Edit"));
        productPermissions.AddChild("Test");
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InventoryResource>(name);
    }
}
