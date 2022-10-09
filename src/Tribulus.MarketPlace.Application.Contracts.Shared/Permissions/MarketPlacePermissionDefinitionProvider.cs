using Tribulus.MarketPlace.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.Permissions;

public class MarketPlacePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var marketPlaceGroup = context.AddGroup(MarketPlacePermissions.GroupName);

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MarketPlaceResource>(name);
    }
}
