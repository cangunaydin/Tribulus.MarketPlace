using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shared.MassTransit;

[DependsOn(
    typeof(AbpAutofacModule)
)]
public class MarketPlaceSharedMassTransitModule:AbpModule
{

}