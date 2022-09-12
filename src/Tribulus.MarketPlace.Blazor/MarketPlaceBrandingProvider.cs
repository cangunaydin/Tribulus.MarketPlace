using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tribulus.MarketPlace.Blazor;

[Dependency(ReplaceServices = true)]
public class MarketPlaceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MarketPlace";
}
