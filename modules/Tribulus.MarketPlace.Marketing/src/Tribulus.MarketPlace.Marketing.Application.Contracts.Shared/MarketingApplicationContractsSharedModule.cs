using System;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Marketing.Application.Contracts.Shared
{
    [DependsOn(
    typeof(MarketingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]

    public class MarketingApplicationContractsSharedModule:AbpModule
    {

    }
}
