using Tribulus.MarketPlace.Shipping.Localization;
using Tribulus.MarketPlace.Shipping.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Shipping.Application.Contracts.Shared.Permissions
{
    public class ShippingPermissionsDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var inventoryGroup = context.AddGroup(ShippingPermissions.GroupName, L("Permission:Inventory"));


            var productPermissions = inventoryGroup.AddPermission(ShippingPermissions.ProductShippingOptions.Default, L("Permission:ProductStocksManagement"));
            productPermissions.AddChild(ShippingPermissions.ProductShippingOptions.Update, L("Permission:Edit"));
            productPermissions.AddChild(ShippingPermissions.ProductShippingOptions.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ShippingResource>(name);
        }
    }
}
