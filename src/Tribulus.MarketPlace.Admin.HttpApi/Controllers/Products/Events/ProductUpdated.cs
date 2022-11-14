using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Events
{
    public interface ProductUpdated
    {
        public ProductCompositionDto Product { get; }
    }

}
