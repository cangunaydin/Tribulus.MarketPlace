using Autofac;
using MassTransit;
using MassTransit.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tribulus.MarketPlace.Admin.Components.Consumers;
using Tribulus.MarketPlace.Admin.Components.ItineraryPlanners;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Components.Activities;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Marketing.Components.Activities;
using Tribulus.MarketPlace.Admin.Models;
using Tribulus.MarketPlace.Admin.Products.StateMachine;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Shipping;
using Tribulus.MarketPlace.Admin.StateMachine;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.Admin;

[DependsOn(
    typeof(AdminMarketingApplicationModule),
    typeof(AdminSalesApplicationModule),
    typeof(AdminShippingApplicationModule),
    typeof(AdminInventoryApplicationModule),
    typeof(MarketPlaceDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(AdminApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class MarketPlaceAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MarketPlaceAdminApplicationModule>();
        });
        ConfiguteMassTransitWithMediatR(context);

    }


    private void ConfiguteMassTransitWithMediatR(ServiceConfigurationContext context)
    {
        var containerBuilder = context.Services.GetContainerBuilder();

        containerBuilder.RegisterType<ProductSagaDefinition>();
        containerBuilder.RegisterType<RequestSagaDefinition>();

        context.Services.TryAddScoped<IRoutingSlipItineraryPlanner<Product>, ProductItineraryPlanner>();

        context.Services.AddMassTransit(cfg =>
        {
            cfg.ApplyCustomMassTransitConfiguration();

            cfg.AddDelayedMessageScheduler();

            //cfg.AddConsumers(Assembly.GetExecutingAssembly());

            //cfg.AddActivities(Assembly.GetExecutingAssembly());

            cfg.AddConsumersFromNamespaceContaining<SubmitProductConsumer>();
            cfg.AddConsumersFromNamespaceContaining<SubmitProductResponseConsumer>();

            cfg.AddActivity(typeof(ProductMarketingActivity)).ExecuteEndpoint(e =>
            {
                e.Name = EndpointsUri.ProductMarketingActivityUri;
            });

            cfg.AddActivity(typeof(ProductInventoryActivity)).ExecuteEndpoint(e =>
            {
                e.Name = EndpointsUri.ProductInventoryActivityUri;
            });


            cfg.AddSagaStateMachine<ProductStateMachine, ProductState>(typeof(ProductSagaDefinition))
                       .InMemoryRepository();

            cfg.AddSagaStateMachine<RequestStateMachine, RequestState>(typeof(RequestSagaDefinition))
                .InMemoryRepository();

            //cfg.AddSagaStateMachine<ProductCourierStateMachine, ProductTransactionState>()
            // .InMemoryRepository();


            //cfg.SetInMemorySagaRepositoryProvider();

            cfg.UsingInMemory((c, inmcfg) =>
            {
                inmcfg.AutoStart = true;

                inmcfg.UseInstrumentation();

                //inmcfg.ApplyCustomBusConfiguration();

                //if (IsRunningInContainer)
                //    cfg.Host("rabbitmq");

                inmcfg.UseDelayedMessageScheduler();

                inmcfg.ConfigureEndpoints(c);
            });

            cfg.AddRequestClient<SubmitProduct>();
            cfg.AddRequestClient<RequestProduct>();

        });

        //context.Services.Configure<MassTransitHostOptions>(options =>
        //{
        //    options.WaitUntilStarted = true;
        //    options.StartTimeout = TimeSpan.FromSeconds(30);
        //    options.StopTimeout = TimeSpan.FromMinutes(1);
        //});

        //var busControl = context.Services.GetRequiredService<IBusControl>();

        //var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        //await busControl.StartAsync(source.Token);
    }

}
