using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tribulus.MarketPlace.Extensions;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shared.MassTransit;

[DependsOn(
    typeof(AbpAutofacModule)
)]
public class MarketPlaceSharedMassTransitModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
//        context.Services.AddMassTransit(cfg =>
//        {
//            cfg.ApplyMarketPlaceMassTransitConfiguration();

//            cfg.TryAddScoped<IItineraryPlanner<CreateProduct>, CreateProductItineraryPlanner>();

//            //cfg.AddActivitiesFromNamespaceContaining<CreateProductActivity>();
//            //cfg.AddActivitiesFromNamespaceContaining<CreateProductPriceActivity>();
//            //cfg.AddActivitiesFromNamespaceContaining<CreateProductStockActivity>();
//            cfg.AddFuturesFromNamespaceContaining<CreateProductFuture>();
//            cfg.AddActivity(typeof(CreateProductStockActivity)).ExecuteEndpoint(e =>
//            {
//        e.Name = "product-inventory-execute";
//    });
//            cfg.AddSagaRepository<FutureState>()
//                .InMemoryRepository();

//    cfg.UsingInMemory((context, cfg) =>
//            {
//                // Controllers are using the request client, so we may as well
//                // start the bus receive endpoint
//                cfg.AutoStart = true;

//                cfg.ConfigureEndpoints(context);
//            });

//            cfg.AddRequestClient<CreateProduct>();

//        });

}
}
