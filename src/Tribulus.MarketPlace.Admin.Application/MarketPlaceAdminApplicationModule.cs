using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Tribulus.MarketPlace.Admin.Components.ItineraryPlanners;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Futures;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Components.Activities;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Marketing.Components.Activities;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Models;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Shipping;
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

        context.Services.TryAddScoped<IItineraryPlanner<ProductTransactionProduct>, ProductItineraryPlanner>();

        context.Services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            //cfg.AddSagaStateMachine<ProductCourierStateMachine, ProductTransactionState>()
            // .InMemoryRepository();
            cfg.AddConsumers(Assembly.GetExecutingAssembly());
            cfg.AddActivities(Assembly.GetExecutingAssembly());
            cfg.AddFuturesFromNamespaceContaining<ProductTransactionFuture>();
            //cfg.AddConsumer(typeof(ProductTransactionConsumer));
            cfg.AddActivity(typeof(ProductMarketingActivity)).ExecuteEndpoint(e => e.Name = EndpointsUri.ProductMarketingActivityUri);
            cfg.AddActivity(typeof(ProductInventoryActivity)).ExecuteEndpoint(e => e.Name = EndpointsUri.ProductInventoryActivityUri);
            cfg.SetInMemorySagaRepositoryProvider();

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
