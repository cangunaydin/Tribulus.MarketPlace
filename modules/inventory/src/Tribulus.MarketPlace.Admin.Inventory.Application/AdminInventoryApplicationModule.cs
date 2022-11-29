using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tribulus.MarketPlace.Inventory;
using Volo.Abp.Http.Client.Dapr;

namespace Tribulus.MarketPlace.Admin.Inventory;

[DependsOn(
    typeof(InventoryDomainModule),
    typeof(AdminInventoryApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)//,
    //typeof(AbpHttpClientDaprModule)
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
        //context.Services.AddHttpClientProxies(typeof(AdminInventoryApplicationContractsModule).Assembly, "Default");

    }
}
