using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Inventory.Courier.Activities
{
    public static class ProductInventoryActivityExtension
    {
        public static void AddProductInventoryActivity(RoutingSlipBuilder builder, Uri uri, dynamic obj)
        {
            builder.AddActivity("product-inventory-execute", uri, obj);
        }
    }
}
