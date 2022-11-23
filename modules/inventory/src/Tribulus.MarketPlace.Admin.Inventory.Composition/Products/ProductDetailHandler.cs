using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products.LocalEvents;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Inventory.Products;

public class ProductDetailHandler : INotificationHandler<GetProductDetailEto>
{
    private readonly IProductStockAppService _productStockAppService;
    private readonly IObjectMapper _objectMapper;
    public ProductDetailHandler(IProductStockAppService productStockAppService, 
        IObjectMapper objectMapper)
    {
        _productStockAppService = productStockAppService;
        _objectMapper = objectMapper;
    }

    public async Task Handle(GetProductDetailEto notification, CancellationToken cancellationToken)
    {
        var id = notification.Id;
        var productStock = await _productStockAppService.GetAsync(id);
        _objectMapper.Map(productStock, notification.Product);
    }
}
