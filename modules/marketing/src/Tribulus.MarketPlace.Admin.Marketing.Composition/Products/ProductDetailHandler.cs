using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products.LocalEvents;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Marketing.Products;

public class ProductDetailHandler : INotificationHandler<GetProductDetailEto>
{
    private readonly IProductAppService _productAppService;
    private readonly IObjectMapper _objectMapper;

    public ProductDetailHandler(IProductAppService productAppService, IObjectMapper objectMapper)
    {
        _productAppService = productAppService;
        _objectMapper = objectMapper;
    }

    public async Task Handle(GetProductDetailEto notification, CancellationToken cancellationToken)
    {
        var id = notification.Id;
        var product = await _productAppService.GetAsync(id);
        _objectMapper.Map(product, notification.Product);

    }
}
