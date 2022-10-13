using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Marketing.Products;

namespace Tribulus.MarketPlace.Admin.Events
{
  
    public class ProductCreateRequested : IRequest<ProductViewModelCompositionDto>
    {
        public ProductViewModelCompositionDto Product { get; set; }
    }

}
