using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Sales.Orders;

public class OrderFilterDto:PagedResultRequestDto
{
    public string Name { get; set; }
}
