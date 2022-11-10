using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Emailing;

namespace Tribulus.MarketPlace.Inventory;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(InventoryDomainSharedModule)
)]
public class InventoryDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        //Configure<AbpMultiTenancyOptions>(options =>
        //{
        //    options.IsEnabled = MultiTenancyConsts.IsEnabled;
        //});

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }
}