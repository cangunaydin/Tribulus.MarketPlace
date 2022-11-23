using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products.LocalEvents;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public class ProductDetailHandler : INotificationHandler<GetProductDetailEto>
    {
        private readonly IProductPriceAppService _productPriceAppService;
        private readonly IObjectMapper _objectMapper;

        public ProductDetailHandler(IProductPriceAppService productPriceAppService, IObjectMapper objectMapper)
        {
            _productPriceAppService = productPriceAppService;
            _objectMapper = objectMapper;
        }
        public async Task Handle(GetProductDetailEto notification, CancellationToken cancellationToken)
        {
            var id = notification.Id;
            var productPrice = await _productPriceAppService.GetAsync(id);
            _objectMapper.Map(productPrice, notification.Product);
        }
    }
}
