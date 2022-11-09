using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Products.MassTransit.Events
{
    public interface ProductCreated
    {
        public ProductCompositionDto Product { get; }
    }
}
