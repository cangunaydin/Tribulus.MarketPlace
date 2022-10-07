using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Marketing
{
    public class ProductListFilterDto : PagedResultRequestDto
    {
        public string Name { get; set; }
    }
}
