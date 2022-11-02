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
using Tribulus.MarketPlace.Admin.Products.Events;
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
    typeof(AdminInventoryApplicationModule),
    typeof(AdminShippingApplicationModule),
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

        ConfigureMassTransitWithMediatR(context);

    }

    private void ConfigureMassTransitWithMediatR(ServiceConfigurationContext context)
    {
        context.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingInMemory((c, inmcfg) =>
            {
                inmcfg.ConfigureEndpoints(c);
            });

            x.AddSagaStateMachine<ProductCourierStateMachine, ProductTransactionState>()
             .InMemoryRepository();
            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.AddActivities(Assembly.GetExecutingAssembly());
            x.AddConsumer(typeof(ProductTransactionConsumer));
            x.AddActivity(typeof(ProductMarketingActivity)).ExecuteEndpoint(e => e.Name = EndpointsUri.ProductMarketingActivityUri);
            x.AddActivity(typeof(ProductInventoryActivity)).ExecuteEndpoint(e => e.Name = EndpointsUri.ProductInventoryActivityUri);

            x.AddRequestClient<SubmitProductRequest>();
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
