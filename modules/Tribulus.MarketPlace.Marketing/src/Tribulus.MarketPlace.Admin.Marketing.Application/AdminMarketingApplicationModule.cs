using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Marketing;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
    typeof(MarketingDomainModule),
    typeof(AdminMarketingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
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
