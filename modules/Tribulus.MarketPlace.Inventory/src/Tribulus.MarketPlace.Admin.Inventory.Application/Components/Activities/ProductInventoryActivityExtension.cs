using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Inventory.Components.Activities
{
    public static class ProductInventoryActivityExtension
    {
        public static void AddProductInventoryActivity(IItineraryBuilder builder, Uri uri, dynamic obj)
        {
            builder.AddActivity("product-inventory-execute", uri, obj);
        }
    }
}
