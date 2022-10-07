using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Marketing
{
    public class ProductDto : EntityDto<Guid>
    {
        public string Name { get;  set; }

        public string Description { get; set; }

    }
}
