using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Sales;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SalesDomainSharedModule)
)]
public class SalesDomainModule : AbpModule
{

}
