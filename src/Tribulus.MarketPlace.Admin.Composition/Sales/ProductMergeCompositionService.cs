using System;
using System.Threading.Tasks;
using Tribulus.Composition;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Sales.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp;
using Microsoft.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Sales
{
    public class ProductMergeCompositionService : CompositionService, 
        IProductDetailService, 
        ICompositionHandleService,
        IProductCreateService,
        IProductListService,
        IRemoteService,
        ITransientDependency
    {
        private readonly IProductPriceAppService _productPriceAppService;
        public ProductMergeCompositionService(IProductPriceAppService productPriceAppService)
        {
            _productPriceAppService = productPriceAppService;
        }

        public async Task CreateAsync(Guid id, [FromBody] Admin.Products.CreateProductDto input)
        {
            var c = new CreateProductDto();
        }

        [ViewModelProperty(nameof(ProductCompositionDto.ProductPrice))]
        public async Task<ProductCompositionDto> GetAsync(Guid id)
        {
            var result = CompositionContext.HttpRequest.GetComposedResponseModel<ProductCompositionDto>();
            result.ProductPrice = await _productPriceAppService.GetAsync(id);
            return result;
        }

        public Task<ProductListDto> GetListAsync(ProductFilterDto input)
        {
            throw new NotImplementedException();
        }
    }
}
