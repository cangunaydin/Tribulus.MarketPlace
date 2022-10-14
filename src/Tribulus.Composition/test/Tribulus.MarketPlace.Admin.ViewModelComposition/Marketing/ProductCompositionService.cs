using Microsoft.AspNetCore.Authorization;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.ViewModelComposition;
using Tribulus.MarketPlace.Marketing;
using Tribulus.ServiceComposer;
using Volo.Abp.Domain.Repositories;

namespace Tribulus.MarketPlace.Admin.Marketing;

public class ProductCompositionService : CompositionService, IProductCompositionService, ICompositionHandleService
{
    private readonly IRepository<Product,Guid> _productRepository;
    public ProductCompositionService(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    [ViewModelProperty(nameof(ProductViewModelCompositionDto.Product))]
    public Task<ProductViewModelCompositionDto> GetAsync(Guid id)
    {
        var viewModel = CompositionContext.HttpRequest.GetComposedResponseModel<ProductViewModelCompositionDto>();
        viewModel.Product = new ProductDto() { Name = "Test 1", Description = "Description 1" };
        return Task.FromResult(viewModel);
    }

    [Authorize]
    public async Task<ProductListDto> GetProducts(ProductFilterDto input)
    {
        var query = await _productRepository.GetQueryableAsync();
        var products = await AsyncExecuter.ToListAsync(query);
        var viewModel = CompositionContext.HttpRequest.GetComposedResponseModel<ProductListDto>();
        viewModel.Products = new List<ProductViewModelCompositionDto>()
        {
            new ProductViewModelCompositionDto()
            {
                Product=new ProductDto()
                {
                    Id=GuidGenerator.Create(),
                    Name="Test 1"
                }

            },
            new ProductViewModelCompositionDto()
            {
                Product=new ProductDto()
                {
                    Id=GuidGenerator.Create(),
                    Name="Test 2"
                }

            }
        };
        //await CompositionContext.RaiseEvent(new ProductListRequested() { ProductIds = viewModel.Products.Select(o => o.Product.Id).ToList() });
        return viewModel;
    }
}

