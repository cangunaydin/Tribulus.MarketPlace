using GettingStarted;
using Localization.Resources.AbpUi;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tribulus.MarketPlace.Admin.Courier.Activities;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Composition;
using Tribulus.MarketPlace.Admin.Marketing;
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
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddSagaStateMachine<ProductCourierStateMachine, ProductTransactionState>()
                .InMemoryRepository();
            cfg.UsingInMemory((c, inmcfg) =>
            {
                inmcfg.ConfigureEndpoints(c);
            });          
        });

        //context.Services.AddMassTransitHostedService();
        context.Services.AddScoped<ProductTransactionSubmittedActivity>();
        context.Services.AddMediatR(typeof(AdminHttpApiModule).Assembly);
        context.Services.AddHostedService<Worker>();
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
