using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Inventory.Composition
{
    [DependsOn(
    typeof(AdminShippingCompositionAutoMapperProfile),
    typeof(AbpAutoMapperModule)
    )]
    public class AdminShippingCompositionModule:AbpModule
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