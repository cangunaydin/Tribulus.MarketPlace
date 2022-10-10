using System.Collections.Generic;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Events;

public class SubscribeGetProductListEto
{
    public List<ProductCompositionDto> Products { get; set; }
}
