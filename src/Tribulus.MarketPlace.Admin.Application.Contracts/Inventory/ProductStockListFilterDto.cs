using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Inventory
{
    public class ProductStockListFilterDto : LimitedResultRequestDto
    {
        public List<Guid> Ids { get; set; }
    }
}
