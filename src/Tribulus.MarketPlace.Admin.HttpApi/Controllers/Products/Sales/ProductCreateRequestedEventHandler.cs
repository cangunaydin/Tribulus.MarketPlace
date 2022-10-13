using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.Controllers.Products.Sales
{
    public class ProductCreateRequestedEventHandler : IRequestHandler<ProductCreateRequested, ProductViewModelCompositionDto>
    {
        private readonly IProductAppService _productAppService;

        public Task<ProductViewModelCompositionDto> Handle(ProductCreateRequested request, CancellationToken cancellationToken)
        {
            return null;
            //return request;
        }

    }
}
