using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MarketingHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class MarketingConsoleApiClientModule : AbpModule
{

}
