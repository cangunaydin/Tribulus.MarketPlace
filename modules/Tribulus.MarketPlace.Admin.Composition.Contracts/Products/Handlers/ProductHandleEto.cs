using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Products.Handlers
{
    public class ProductHandleEto:IRequest<ProductCompositionDto>
    {
        public string Name { get; set; }
    }
}
