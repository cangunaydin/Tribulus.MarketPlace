using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tribulus.MarketPlace.Inventory;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Tribulus.MarketPlace.Admin.Inventory;

[DependsOn(
    typeof(InventoryDomainModule),
    typeof(AdminInventoryApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule)
    )]
public class AdminInventoryApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdminInventoryApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminInventoryApplicationModule>(validate: true);
        });

    }
}
