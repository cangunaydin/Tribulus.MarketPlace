using MassTransit.Courier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public interface IUpdateProductStockActivity : IActivity<ProductStockArguments, ProductStockLog>
    {
    }

}
