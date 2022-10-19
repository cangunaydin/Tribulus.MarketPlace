using MediatR;
using System.Collections.Generic;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Events;

public class ProductListRequested:INotification
{
    public List<ProductViewModelCompositionDto> Products { get; set; }
}
