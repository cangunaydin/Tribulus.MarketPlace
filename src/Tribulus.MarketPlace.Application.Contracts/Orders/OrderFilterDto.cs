using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Orders
{
    public class OrderFilterDto:PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}
