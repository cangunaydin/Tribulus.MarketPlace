using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Sales
{
    public class ProductPriceListFilterDto : LimitedResultRequestDto
    {
        public List<Guid> Ids { get; set; }
    }
}
