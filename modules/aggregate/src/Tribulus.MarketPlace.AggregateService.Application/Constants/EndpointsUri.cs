using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.AggregateService.Constants
{
    public static class EndpointsUri
    {
        public const string MainUri = "loopback://localhost/";

        public const string RabbitMqMainUri = "rabbitmq://localhost/";

        public const string ProductMarketingActivityUri = "product-marketing-execute";

        public const string ProductSalesActivityUri = "product-sales-execute";

        public const string ProductInventoryActivityUri = "product-inventory-execute";

        //public const string ProductShippingActivityUri = "product-shipping-execute";
    }
}
