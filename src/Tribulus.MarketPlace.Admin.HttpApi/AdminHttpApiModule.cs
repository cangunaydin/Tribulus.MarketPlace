using Localization.Resources.AbpUi;
using MassTransit;
using MediatR;
using Tribulus.MarketPlace.Admin.Inventory;
using Tribulus.MarketPlace.Admin.Inventory.Composition;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.MassTransit.Products.Consumers;
using Tribulus.MarketPlace.Admin.Sales;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Tribulus.MarketPlace.Extensions;
using Tribulus.MarketPlace.Localization;
using Tribulus.MarketPlace.Products.MassTransit.Commands;
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
    typeof(AdminApplicationContractsModule),
    typeof(SharedContractsModule),
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
        ConfiguteMediatr(context);
    }

    private void ConfiguteMediatr(ServiceConfigurationContext context)
    {
        context.Services.AddMediatR(typeof(AdminHttpApiModule));
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
