using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.AggregateService
{
    public interface GetProduct
    {
        public Guid ProductId { get; set; }
    }
}
