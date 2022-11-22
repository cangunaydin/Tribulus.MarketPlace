using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Inventory.Products
{
    public class ProductStockListFilterDto : LimitedResultRequestDto
    {
        public List<Guid> Ids { get; set; }
    }
}
