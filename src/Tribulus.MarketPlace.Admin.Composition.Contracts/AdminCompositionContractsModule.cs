using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin
{
    [DependsOn(typeof(AbpDddApplicationContractsModule))]
    public class AdminCompositionContractsModule:AbpModule
    {
    }
}
