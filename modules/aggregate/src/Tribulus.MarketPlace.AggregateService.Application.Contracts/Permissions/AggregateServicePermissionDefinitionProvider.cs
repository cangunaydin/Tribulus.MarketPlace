using Tribulus.MarketPlace.AggregateService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.AggregateService.Permissions;

public class AggregateServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AggregateServicePermissions.GroupName, L("Permission:AggregateService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AggregateServiceResource>(name);
    }
}
