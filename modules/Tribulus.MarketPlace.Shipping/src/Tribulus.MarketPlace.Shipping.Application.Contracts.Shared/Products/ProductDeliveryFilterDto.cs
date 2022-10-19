using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Shipping.Products
{
    public class ProductDeliveryFilterDto : LimitedResultRequestDto
    {
        public List<Guid> Ids { get; set; }
    }
}
