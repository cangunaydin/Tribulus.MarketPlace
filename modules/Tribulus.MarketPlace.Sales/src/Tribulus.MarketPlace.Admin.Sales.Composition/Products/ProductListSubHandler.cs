
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products.LocalEvents;
using Tribulus.MarketPlace.Sales.Products;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Sales.Products
{
    public class ProductListSubHandler : INotificationHandler<ProductListSub>
    {
        private readonly IProductPriceAppService _productPriceAppService;

        private readonly IObjectMapper _objectMapper;
        public ProductListSubHandler(IProductPriceAppService productPriceAppService, IObjectMapper objectMapper)
        {
            _productPriceAppService = productPriceAppService;
            _objectMapper = objectMapper;
        }

        public async Task Handle(ProductListSub notification, CancellationToken cancellationToken)
        {
            var productCompositions = notification.Products;
            var productPricesInput = new ProductPriceListFilterDto();
            productPricesInput.Ids = notification.Products.Select(o => o.Id).ToList();
            var productPricesResult = await _productPriceAppService.GetListAsync(productPricesInput);
            foreach (var productPrice in productPricesResult.Items)
            {
                var productComposition = productCompositions.FirstOrDefault(o => o.Id == productPrice.Id);
                productComposition=_objectMapper.Map(productPrice, productComposition);
            }
        }
    }
}
