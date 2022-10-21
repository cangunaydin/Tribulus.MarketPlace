using MassTransit;
using MediatR;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin
{
    [DependsOn(typeof(AbpDddApplicationContractsModule))]
    public class AdminCompositionContractsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMassTransit();
            context.Services.AddMediatR(typeof(AdminCompositionContractsModule));
        }
    }
}
