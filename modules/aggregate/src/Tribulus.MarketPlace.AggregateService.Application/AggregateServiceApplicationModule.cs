using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Inventory;
using MediatR;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AggregateServiceDomainModule),
    typeof(AggregateServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),

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

        //apply masstransit config over here.

    }
}
