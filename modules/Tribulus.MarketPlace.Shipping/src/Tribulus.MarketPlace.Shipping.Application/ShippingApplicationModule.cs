using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(ShippingDomainModule),
    typeof(ShippingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class ShippingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ShippingApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ShippingApplicationModule>(validate: true);
        });
    }
}
