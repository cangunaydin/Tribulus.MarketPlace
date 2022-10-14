
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.ViewModelComposition;
using Tribulus.ServiceComposer;

namespace Tribulus.MarketPlace.Admin.Sales;


public class ProductCompositionService : CompositionService, IProductCompositionService,ICompositionHandleService
{
    public ProductCompositionService()
    {
    }
    [ViewModelProperty(nameof(ProductViewModelCompositionDto.ProductPrice))]
    public Task<ProductViewModelCompositionDto> GetAsync(Guid id)
    {
        var viewModel = CompositionContext.HttpRequest.GetComposedResponseModel<ProductViewModelCompositionDto>();
        viewModel.ProductPrice = new ProductPriceDto() { Price = 500 };
        return Task.FromResult(viewModel);
    }
}
