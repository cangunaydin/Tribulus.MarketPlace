using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Shipping.Composition
{
    [DependsOn(
    typeof(AdminShippingApplicationContractsModule),
    typeof(AbpAutoMapperModule)
    )]
    public class AdminShippingCompositionModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMediatR(typeof(AdminShippingCompositionModule));
            context.Services.AddAutoMapperObjectMapper<AdminShippingCompositionModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AdminShippingCompositionModule>(validate: true);
            });
        }
    }
}