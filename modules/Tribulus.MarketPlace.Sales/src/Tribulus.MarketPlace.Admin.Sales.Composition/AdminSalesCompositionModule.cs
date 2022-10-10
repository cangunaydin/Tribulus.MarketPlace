using System;
using Volo.Abp.Modularity;

namespace Tribulus.MarketPlace.Admin.Sales.Composition
{
    [DependsOn(typeof(AdminSalesApplicationContractsModule))]
    public class AdminSalesCompositionModule:AbpModule
    {

    }
}
