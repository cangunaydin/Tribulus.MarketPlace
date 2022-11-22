using Tribulus.MarketPlace.AbpService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tribulus.MarketPlace.AbpService.Permissions;

public class AbpServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpServicePermissions.GroupName, L("Permission:AbpService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpServiceResource>(name);
    }
}
