using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Shipping;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ShippingHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class ShippingConsoleApiClientModule : AbpModule
{

}
