using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tribulus.MarketPlace.Admin.Blazor;

[Dependency(ReplaceServices = true)]
public class AdminBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Admin";
}
