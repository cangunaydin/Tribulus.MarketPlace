using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tribulus.Composition;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Tribulus.MarketPlace.Admin.Products;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using CreateProductDto = Tribulus.MarketPlace.Admin.Products.CreateProductDto;

namespace Tribulus.MarketPlace.Admin.Marketing
{
    public class ProductMergeCompositionService : CompositionService,
            IProductDetailService,
        IProductDetailServiceAnother,
        IProductCreateService,
         ICompositionHandleService,
        IRemoteService,
        ITransientDependency
    {
        private readonly IProductAppService _productAppService;
        public ProductMergeCompositionService(IProductAppService productPriceAppService)
        {
            _productAppService = productPriceAppService;
        }

        public async Task CreateAsync(Guid id, [FromBody] Admin.Products.CreateProductDto input)
        {
            var c = new CreateProductDto();
        }

        public async Task GetAnotherAsync(Guid id)
        {
            await _productAppService.GetAsync(id);
            var result = CompositionContext.HttpRequest.GetComposedResponseModel();
        }

        [ViewModelProperty(nameof(ProductCompositionDto.Product))]
        public async Task<ProductCompositionDto> GetAsync(Guid id)
        {
            var result = CompositionContext.HttpRequest.GetComposedResponseModel<ProductCompositionDto>();
            result.Product = await _productAppService.GetAsync(id);
            return result;
        }
    }
}
