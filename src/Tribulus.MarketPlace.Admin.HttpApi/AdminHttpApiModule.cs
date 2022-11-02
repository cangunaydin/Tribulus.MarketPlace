using Localization.Resources.AbpUi;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Composition;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Shipping;
using Tribulus.MarketPlace.Admin.Shipping.Composition;
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
    typeof(AdminInventoryHttpApiModule),
    typeof(AdminShippingHttpApiModule),
    typeof(AdminApplicationContractsModule),
    typeof(AdminCompositionContractsModule),
    typeof(AdminMarketingCompositionModule),
    typeof(AdminSalesCompositionModule),
    typeof(AdminShippingCompositionModule),
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
