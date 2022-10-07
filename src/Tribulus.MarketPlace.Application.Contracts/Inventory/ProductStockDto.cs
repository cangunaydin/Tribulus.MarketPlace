using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Inventory
{
    public class ProductStockDto : EntityDto<Guid>
    {
        public int StockCount { get;  set; }
    }
}
