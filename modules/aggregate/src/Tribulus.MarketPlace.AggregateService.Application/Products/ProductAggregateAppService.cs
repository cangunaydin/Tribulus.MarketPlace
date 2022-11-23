using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Inventory.Products;
using Tribulus.MarketPlace.Admin.Marketing.Products;

namespace Tribulus.MarketPlace.AggregateService.Products;

public class ProductAggregateAppService : AggregateServiceAppService, IProductAggregateAppService
{
    private readonly IProductStockAppService _productStockAppService;
    private readonly IProductAppService _productAppService;
    public ProductAggregateAppService(IProductStockAppService productStockAppService, IProductAppService productAppService)
    {
        _productStockAppService = productStockAppService;
        _productAppService = productAppService;
    }

    public async Task GetProducts()
    {
        await _productAppService.GetListAsync(new Marketing.Products.ProductListFilterDto() { MaxResultCount = 100 });

    }
}
