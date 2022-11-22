using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tribulus.MarketPlace;

[Dependency(ReplaceServices = true)]
public class MarketPlaceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MarketPlace";
}
