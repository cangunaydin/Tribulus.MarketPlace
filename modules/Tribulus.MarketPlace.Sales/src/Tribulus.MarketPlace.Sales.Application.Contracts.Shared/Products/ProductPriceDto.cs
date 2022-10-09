using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Sales.Products
{
    public class ProductPriceDto : EntityDto<Guid>
    {
        public decimal Price { get; set; }
    }
}
