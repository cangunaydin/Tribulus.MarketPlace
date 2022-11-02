using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Products
{
    public class ProductCompositionSaveDto : EntityDto<Guid>
    { 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public string ShippingName { get; set; }
    }
}
