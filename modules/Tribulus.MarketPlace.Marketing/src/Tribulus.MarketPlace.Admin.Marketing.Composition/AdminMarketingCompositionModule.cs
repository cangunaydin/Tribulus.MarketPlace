using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
typeof(AdminMarketingApplicationContractsModule),
typeof(AbpAutoMapperModule)
)]
public class AdminMarketingCompositionModule:AbpModule
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