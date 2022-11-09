using MediatR;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class SharedContractsModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMediatR(typeof(SharedContractsModule));
    }
}
