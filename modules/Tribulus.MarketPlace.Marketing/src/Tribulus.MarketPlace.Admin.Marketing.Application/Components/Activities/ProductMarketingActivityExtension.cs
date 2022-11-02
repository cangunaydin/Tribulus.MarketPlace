using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Marketing.Components.Activities
{
    public static class ProductMarketingActivityExtension
    {
        public static void AddProductMarketingActivity(IItineraryBuilder builder, Uri uri, dynamic obj)
        {
            builder.AddActivity("product-marketing-execute", uri, obj);
        }
    }
}
