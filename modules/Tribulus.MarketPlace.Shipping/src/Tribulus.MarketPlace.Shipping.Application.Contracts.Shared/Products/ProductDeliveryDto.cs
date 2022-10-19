using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductDeliveryDto : EntityDto<Guid>
    {
        public string ShippingName { get; set; }

        public int MaxDays { get; set; }

        public int MinDays { get; set; }


    }
}
