using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Events
{

    public interface ProductUpdateFaulted
    {
        public Guid ProductId { get; }

        public string Reason { get; }

    }


}
