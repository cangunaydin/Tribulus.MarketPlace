using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Products.MassTransit.Events
{
    public interface ProductCreationFaulted
    {
        public Guid ProductId { get; }

        public string Reason { get; }

    }
}
