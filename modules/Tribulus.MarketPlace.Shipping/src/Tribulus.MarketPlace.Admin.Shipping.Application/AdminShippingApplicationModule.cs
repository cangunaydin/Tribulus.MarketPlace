using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tribulus.MarketPlace.Shipping;

namespace Tribulus.MarketPlace.Admin.Shipping;

[DependsOn(
    typeof(ShippingDomainModule),
    typeof(ShippingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class AdminShippingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdminShippingApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminShippingApplicationModule>(validate: true);
        });
    }
}
