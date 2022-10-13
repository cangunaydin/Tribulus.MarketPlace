using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribulus.MarketPlace.Admin.Products
{
    public interface IProductCreateService
    {
        Task CreateAsync(Guid id, CreateProductDto input);
    }
}
