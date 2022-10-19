using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductDelivery : FullAuditedAggregateRoot<Guid>
    {
        public string ShippingName { get; private set; }

        public int MaxDays { get; private set; }

        public int MinDays { get; private set; }

        private ProductDelivery() { }

        public ProductDelivery(Guid id,string shippingName,
            int minDays, int maxDays) : base(id)
        {
            UpdateShippingName(shippingName);
            UpdateMaxDays(maxDays);
            UpdateMinDays(minDays);
        }
        public void UpdateMinDays(int minDays)
        { 
            MinDays = minDays;
        }

        public void UpdateMaxDays(int maxDays)
        {
            MaxDays = maxDays;
        }

        public void UpdateShippingName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name), ProductShippingConsts.MaxNameLength, ProductShippingConsts.MinNameLength);
            ShippingName = name;
        }


    }
}
