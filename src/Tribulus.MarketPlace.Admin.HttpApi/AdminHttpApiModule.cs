using Localization.Resources.AbpUi;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Tribulus.MarketPlace.Admin.Courier.Activities;
using Tribulus.MarketPlace.Admin.Courier.Consumers;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Composition;
using Tribulus.MarketPlace.Admin.Inventory.Courier.Activities;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Marketing.Courier.Activities;
using Tribulus.MarketPlace.Admin.Products.Saga;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Shipping;
using Tribulus.MarketPlace.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.Admin;

[DependsOn(
    typeof(AdminMarketingHttpApiModule),
    typeof(AdminSalesHttpApiModule),
    typeof(AdminShippingHttpApiModule),
    typeof(AdminInventoryHttpApiModule),
    typeof(AdminApplicationContractsModule),
    typeof(AdminCompositionContractsModule),
    typeof(AdminMarketingCompositionModule),
    typeof(AdminSalesCompositionModule),
    typeof(AdminInventoryCompositionModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class AdminHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
        ConfiguteMassTransitWithMediatR(context);
    }

    private void ConfiguteMassTransitWithMediatR(ServiceConfigurationContext context)
    {
        context.Services.AddMassTransit(cfg =>
        {
            cfg.AddConsumers(Assembly.GetExecutingAssembly());
            cfg.AddActivities(Assembly.GetExecutingAssembly());
            cfg.AddActivities(typeof(ProductTransactionConsumer));
            cfg.AddActivities(typeof(ProductTransactionActivity));
            cfg.AddActivities(typeof(ProductMarketingActivity));
            cfg.AddActivities(typeof(ProductInventoryActivity));
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddSagaStateMachine<ProductCourierStateMachine, ProductTransactionState>()
                .InMemoryRepository();
            cfg.UsingInMemory((c, inmcfg) =>
            {
                inmcfg.ConfigureEndpoints(c);
            });          
        });
        context.Services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

        context.Services.AddMediatR(typeof(AdminHttpApiModule).Assembly);
        
        //var busControl = context.Services.GetRequiredService<IBusControl>();

        //var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        //await busControl.StartAsync(source.Token);
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MarketPlaceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
