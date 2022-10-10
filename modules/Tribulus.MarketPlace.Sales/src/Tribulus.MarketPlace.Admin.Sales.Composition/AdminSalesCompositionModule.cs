using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Composition;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Marketing.Composition
{
    [DependsOn(typeof(AdminSalesApplicationContractsModule),
        typeof(AdminCompositionContractsModule),
        typeof(MarketPlaceCompositionModule))]
    public class AdminSalesCompositionModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AdminSalesCompositionModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AdminSalesCompositionModule>(validate: true);
            });
        }
    }
}