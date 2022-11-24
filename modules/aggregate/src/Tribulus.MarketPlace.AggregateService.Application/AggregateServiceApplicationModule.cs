using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Inventory;
using MediatR;
using Tribulus.MarketPlace.Admin.Inventory.Composition;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AggregateServiceDomainModule),
    typeof(AggregateServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),

    typeof(AdminMarketingCompositionModule),
    typeof(AdminInventoryCompositionModule),
    typeof(AdminSalesCompositionModule),
    typeof(AdminSalesHttpApiClientModule),
    typeof(AdminMarketingHttpApiClientModule),
    typeof(AdminInventoryHttpApiClientModule)
    )]
public class AggregateServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AggregateServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AggregateServiceApplicationModule>(validate: true);
        });
        context.Services.AddMediatR(typeof(AggregateServiceApplicationModule));
        context.Services.AddMediatR(typeof(AdminMarketingCompositionModule));
        context.Services.AddMediatR(typeof(AdminInventoryCompositionModule));
        context.Services.AddMediatR(typeof(AdminSalesCompositionModule));

    }
}
