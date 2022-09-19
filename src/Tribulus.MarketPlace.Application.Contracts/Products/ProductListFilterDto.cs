using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Products
{
    public class ProductListFilterDto : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}
