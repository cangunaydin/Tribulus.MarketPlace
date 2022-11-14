using System;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Inventory.Application.Contracts.Shared;

[DependsOn(
    typeof(InventoryDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class InventoryApplicationContractsSharedModule:AbpModule
{

}
