using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tribulus.MarketPlace.Marketing;
using Volo.Abp.Dapr;

namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
    typeof(MarketingDomainModule),
    typeof(AdminMarketingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpDaprModule)
    )]
public class AdminMarketingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdminMarketingApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminMarketingApplicationModule>(validate: true);
        });
    }
}
