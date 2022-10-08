using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(MarketPlaceDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class MarketPlaceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MarketPlaceDtoExtensions.Configure();
    }
}
