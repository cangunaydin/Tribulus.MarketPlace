using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.AbpService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class AbpServiceConsoleApiClientModule : AbpModule
{

}
