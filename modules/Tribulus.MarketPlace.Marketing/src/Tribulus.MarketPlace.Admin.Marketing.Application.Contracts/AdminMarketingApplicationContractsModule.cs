using Microsoft.Extensions.DependencyInjection;
using System;
using Tribulus.MarketPlace.Marketing.Application.Contracts.Shared;
using Volo.Abp.Modularity;
namespace Tribulus.MarketPlace.Admin.Marketing;

[DependsOn(
    typeof(MarketingApplicationContractsSharedModule)
    )]
public class AdminMarketingApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdminMarketingDtoExtensions.Configure();
    }
}
