using Tribulus.MarketPlace.Sales.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Sales.Permissions;

public class SalesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var salesGroup = context.AddGroup(SalesPermissions.GroupName, L("Permission:Sales"));


        var productPermissions = salesGroup.AddPermission(SalesPermissions.ProductPrices.Default, L("Permission:ProductPrices"));
        productPermissions.AddChild(SalesPermissions.ProductPrices.Create, L("Permission:Create"));
        productPermissions.AddChild(SalesPermissions.ProductPrices.Update, L("Permission:Edit"));

        var orderPermissions = salesGroup.AddPermission(SalesPermissions.Orders.Default, L("Permission:Orders"));
        orderPermissions.AddChild(SalesPermissions.Orders.Create, L("Permission:Create"));
        orderPermissions.AddChild(SalesPermissions.Orders.Update, L("Permission:Edit"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SalesResource>(name);
    }
}
