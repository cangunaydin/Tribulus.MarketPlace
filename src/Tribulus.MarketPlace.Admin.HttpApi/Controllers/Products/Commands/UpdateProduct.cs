using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Commands
{
    public interface UpdateProduct
    {
        public Guid ProductId { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public int StockCount { get; }

        public Guid UserId { get; }
    }

}
