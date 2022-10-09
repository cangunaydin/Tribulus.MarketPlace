using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(MarketPlaceDomainModule),
    typeof(MarketPlaceApplicationContractsModule),
    typeof(AbpAccountApplicationModule)
    )]
public class MarketPlaceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MarketPlaceApplicationModule>();
        });
    }
}
