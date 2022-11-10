using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Emailing;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Marketing;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MarketingDomainSharedModule)
)]
public class MarketingDomainModule : AbpModule
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