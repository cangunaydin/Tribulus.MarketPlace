using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Marketing.Courier.Activities
{
    public static class ProductMarketingActivityExtension
    {
        public static void AddProductMarketingActivity(RoutingSlipBuilder builder, Uri uri, dynamic obj)
        {
            builder.AddActivity("product-marketing-execute", uri, obj);
        }
    }
}
