using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products.Events;
using Tribulus.MarketPlace.Admin.Shipping.Products;
using Tribulus.MarketPlace.Shipping.Products;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Shipping.Products;

public class ProductListSubHandler : INotificationHandler<ProductListSub>
{
    private readonly IObjectMapper _objectMapper;
    private readonly IProductDeliveryAppService _productDeliveryAppService;
    public ProductListSubHandler(IProductDeliveryAppService productDeliveryAppService,
        IObjectMapper objectMapper)
    {
        _productDeliveryAppService = productDeliveryAppService;
        _objectMapper = objectMapper;
    }

    public async Task Handle(ProductListSub notification, CancellationToken cancellationToken)
    {
        var productCompositions = notification.Products;
        var productDeliveryListInput = new ProductDeliveryFilterDto();
        productDeliveryListInput.Ids = notification.Products.Select(o => o.Id).ToList();
        var productDeliveriesResult = await _productDeliveryAppService.GetListAsync(productDeliveryListInput);
        foreach (var productDelivery in productDeliveriesResult.Items)
        {
            var productComposition = productCompositions.FirstOrDefault(o => o.Id == productDelivery.Id);
            productComposition = _objectMapper.Map(productDelivery, productComposition);
        }
    }
}
