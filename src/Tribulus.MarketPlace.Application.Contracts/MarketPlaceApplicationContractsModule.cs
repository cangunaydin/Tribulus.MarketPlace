﻿using Tribulus.MarketPlace.Application.Contracts.Shared;
using Tribulus.MarketPlace.Inventory;
using Tribulus.MarketPlace.Marketing;
using Tribulus.MarketPlace.Sales;
using Tribulus.MarketPlace.Shipping;
using Volo.Abp.Account;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace;

[DependsOn(
    typeof(AbpAccountApplicationContractsModule),
    typeof(MarketPlaceApplicationContractsSharedModule),
    typeof(MarketingApplicationContractsModule),
    typeof(SalesApplicationContractsModule),
    typeof(InventoryApplicationContractsModule),
    typeof(ShippingApplicationContractsModule)
)]
public class MarketPlaceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MarketPlaceDtoExtensions.Configure();
    }
}
