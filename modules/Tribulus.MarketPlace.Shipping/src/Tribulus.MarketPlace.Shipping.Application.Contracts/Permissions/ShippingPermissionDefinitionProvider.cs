using Tribulus.MarketPlace.Shipping.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Shipping.Permissions;

public class ShippingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ShippingPermissions.GroupName, L("Permission:Shipping"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ShippingResource>(name);
    }
}
