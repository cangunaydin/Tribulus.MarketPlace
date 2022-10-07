using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Inventory
{
    public class ProductStockDto : EntityDto<Guid>
    {
        public int StockCount { get;  set; }
    }
}
