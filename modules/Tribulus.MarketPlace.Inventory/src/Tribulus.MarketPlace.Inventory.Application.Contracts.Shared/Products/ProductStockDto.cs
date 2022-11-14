using System;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Inventory.Products
{
    public class ProductStockDto : EntityDto<Guid>
    {
        public int StockCount { get;  set; }
    }
}
