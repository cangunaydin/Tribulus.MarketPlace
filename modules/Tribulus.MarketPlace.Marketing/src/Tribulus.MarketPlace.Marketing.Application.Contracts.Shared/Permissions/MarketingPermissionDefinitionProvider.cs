using Tribulus.MarketPlace.Marketing.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Marketing.Permissions;

public class MarketingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var marketingGroup = context.AddGroup(MarketingPermissions.GroupName, L("Permission:Marketing"));

        var productPermissions = marketingGroup.AddPermission(MarketingPermissions.Products.Default, L("Permission:ProductsManagement"));
        productPermissions.AddChild(MarketingPermissions.Products.Create, L("Permission:Create"));
        productPermissions.AddChild(MarketingPermissions.Products.Update, L("Permission:Edit"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MarketingResource>(name);
    }
}
