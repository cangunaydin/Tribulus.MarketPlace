using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.AggregateService.Constants;
using Tribulus.MarketPlace.AggregateService.Products.Activities;
using Tribulus.MarketPlace.AggregateService.Products.Commands;
using Tribulus.MarketPlace.AggregateService.Products.Futures;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

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

        context.Services.AddMassTransit(cfg =>
        {

            //cfg.AddActivitiesFromNamespaceContaining<CreateProductActivity>();
            //cfg.AddActivitiesFromNamespaceContaining<CreateProductPriceActivity>();
            //cfg.AddActivitiesFromNamespaceContaining<CreateProductStockActivity>();
            cfg.AddFuturesFromNamespaceContaining<CreateProductFuture>();
            cfg.AddActivity(typeof(CreateProductActivity)).ExecuteEndpoint(e =>
            {
                e.Name = EndpointsUri.ProductMarketingActivityUri;
            });

            cfg.AddActivity(typeof(CreateProductStockActivity)).ExecuteEndpoint(e =>
            {
                e.Name = EndpointsUri.ProductInventoryActivityUri;
            });
          

            cfg.AddActivity(typeof(CreateProductPriceActivity)).ExecuteEndpoint(e =>
            {
                e.Name = EndpointsUri.ProductSalesActivityUri;
            });

            cfg.AddSagaRepository<FutureState>()
                .InMemoryRepository();

            cfg.UsingInMemory((context, cfg) =>
            {
                // Controllers are using the request client, so we may as well
                // start the bus receive endpoint
                cfg.AutoStart = true;

                cfg.ConfigureEndpoints(context);
            });

            cfg.AddRequestClient<CreateProduct>();

        });


    }

    //apply masstransit config over here.

}

