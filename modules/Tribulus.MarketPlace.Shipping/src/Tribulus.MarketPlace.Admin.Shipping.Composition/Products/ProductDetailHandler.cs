using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products.Events;
using Tribulus.MarketPlace.Admin.Shipping.Products;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Shipping.Products;

public class ProductDetailHandler : INotificationHandler<ProductDetailEto>
{
    private readonly IProductDeliveryAppService _productDeliveryAppService;
    private readonly IObjectMapper _objectMapper;
    public ProductDetailHandler(IProductDeliveryAppService productDeliveryAppService,
        IObjectMapper objectMapper)
    {
        _productDeliveryAppService = productDeliveryAppService;
        _objectMapper = objectMapper;
    }

    public async Task Handle(ProductDetailEto notification, CancellationToken cancellationToken)
    {
        var id = notification.Id;
        var productDelivery = await _productDeliveryAppService.GetAsync(id);
        _objectMapper.Map(productDelivery, notification.Product);
    }
}
