using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Marketing.Products
{
    public class ProductListFilterDto : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
