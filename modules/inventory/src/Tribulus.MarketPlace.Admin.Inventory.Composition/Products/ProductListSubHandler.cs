using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Inventory.Products;
using Tribulus.MarketPlace.Products.LocalEvents;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Inventory.Products;

public class ProductListSubHandler : INotificationHandler<SubscribeProductListEto>
{
    private readonly IObjectMapper _objectMapper;
    private readonly IProductStockAppService _productStockAppService;
    public ProductListSubHandler(IProductStockAppService productStockAppService,
        IObjectMapper objectMapper)
    {
        _productStockAppService = productStockAppService;
        _objectMapper = objectMapper;
    }

    public async Task Handle(SubscribeProductListEto notification, CancellationToken cancellationToken)
    {
        var productCompositions = notification.Products;
        var productStockListInput = new ProductStockListFilterDto();
        productStockListInput.Ids = notification.Products.Select(o => o.Id).ToList();
        var productStocksResult = await _productStockAppService.GetListAsync(productStockListInput);
        foreach (var productStock in productStocksResult.Items)
        {
            var productComposition = productCompositions.FirstOrDefault(o => o.Id == productStock.Id);
            productComposition = _objectMapper.Map(productStock, productComposition);
        }
    }
}
