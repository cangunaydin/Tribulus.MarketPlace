

using System;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Products
{
    public class ProductCompositionDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal StockCount { get; set; }

    }
}