using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductDeliveryFilterDto : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
