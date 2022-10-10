using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Composition;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Marketing.Composition
{
    [DependsOn(typeof(AdminMarketingApplicationContractsModule),
        typeof(AdminCompositionContractsModule),
        typeof(MarketPlaceCompositionModule))]
    public class AdminMarketingCompositionModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AdminMarketingCompositionModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AdminMarketingCompositionModule>(validate: true);
            });
        }
    }
}