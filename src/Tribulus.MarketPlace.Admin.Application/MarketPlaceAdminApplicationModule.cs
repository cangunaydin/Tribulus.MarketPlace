using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Tribulus.MarketPlace.Admin.Constants;
using Tribulus.MarketPlace.Admin.Courier.Consumers;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Courier.Activities;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Marketing.Courier.Activities;
using Tribulus.MarketPlace.Admin.Products.Saga;
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
        context.Services.AddMassTransit(cfg =>
        {         
            cfg.SetKebabCaseEndpointNameFormatter();
         
            cfg.UsingInMemory((c, inmcfg) =>
            {
                inmcfg.ConfigureEndpoints(c);
            });

            cfg.AddSagaStateMachine<ProductCourierStateMachine, ProductTransactionState>()
             .InMemoryRepository();
            cfg.AddConsumers(Assembly.GetExecutingAssembly());
            cfg.AddActivities(Assembly.GetExecutingAssembly());
            cfg.AddConsumer(typeof(ProductTransactionConsumer));
            cfg.AddActivity(typeof(ProductMarketingActivity)).ExecuteEndpoint(e => e.Name = EndpointsUri.ProductMarketingActivityUri);
            cfg.AddActivity(typeof(ProductInventoryActivity)).ExecuteEndpoint(e => e.Name = EndpointsUri.ProductInventoryActivityUri);


        });

        context.Services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

        //var busControl = context.Services.GetRequiredService<IBusControl>();

        //var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        //await busControl.StartAsync(source.Token);
    }

}
