using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Shipping.Permissions;

public class ShippingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var marketingGroup = context.AddGroup(ShippingPermissions.GroupName, L("Permission:Marketing"));

        var productPermissions = marketingGroup.AddPermission(ShippingPermissions.Products.Default, L("Permission:ProductsManagement"));
        productPermissions.AddChild(ShippingPermissions.Products.Create, L("Permission:Create"));
        productPermissions.AddChild(ShippingPermissions.Products.Update, L("Permission:Edit"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ShippingResource>(name);
    }
}
