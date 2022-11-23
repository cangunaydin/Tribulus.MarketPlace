using MediatR;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class SharedContractsModule:AbpModule
{
  
}
