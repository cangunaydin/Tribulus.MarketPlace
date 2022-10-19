using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Inventory.Composition
{
    [DependsOn(
    typeof(AdminInventoryApplicationContractsModule),
    typeof(AbpAutoMapperModule)
    )]
    public class AdminInventoryCompositionModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMediatR(typeof(AdminInventoryCompositionModule));
            context.Services.AddAutoMapperObjectMapper<AdminInventoryCompositionModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AdminInventoryCompositionModule>(validate: true);
            });
        }
    }
}