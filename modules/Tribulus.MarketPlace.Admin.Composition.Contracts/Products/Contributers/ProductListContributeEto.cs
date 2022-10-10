using MediatR;
using System.Collections.Generic;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Events;

public class ProductListContributeEto:IRequest<List<ProductCompositionDto>>
{
    public List<ProductCompositionDto> Products { get; set; }
}
