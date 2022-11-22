using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AggregateService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AggregateServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class AggregateServiceConsoleApiClientModule : AbpModule
{

}
