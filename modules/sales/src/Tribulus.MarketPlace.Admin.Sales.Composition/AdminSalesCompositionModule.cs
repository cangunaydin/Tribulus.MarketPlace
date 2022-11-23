using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Sales;

[DependsOn(
    typeof(AdminSalesApplicationContractsModule),
    typeof(AbpAutoMapperModule)
    )]
public class AdminSalesCompositionModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //context.Services.AddMediatR(typeof(AdminSalesCompositionModule));
        context.Services.AddAutoMapperObjectMapper<AdminSalesCompositionModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminSalesCompositionModule>(validate: true);
        });
    }
}