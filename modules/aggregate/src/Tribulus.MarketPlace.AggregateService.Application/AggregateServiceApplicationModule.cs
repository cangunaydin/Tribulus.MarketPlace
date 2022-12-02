using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Composition;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.AggregateService.Actors.Products;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Dapr;
using Volo.Abp.Http.Client.Dapr;
using Volo.Abp.Modularity;

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
    typeof(AdminInventoryHttpApiClientModule),
    typeof(AbpDaprModule),
    typeof(AbpHttpClientDaprModule)
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

        context.Services.AddActors(options =>
        {
            options.Actors.RegisterActor<ProductProcessActor>();
        });
    }
}
