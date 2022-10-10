using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Events;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Handlers;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Application.Dtos;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    //public class GetProductListHandler : AdminMarketingCompositionService, IRequestHandler<HandleGetProductListEto, PagedResultDto<ProductCompositionDto>>
    //{
    //    private readonly IProductAppService _productAppService;
    //    private readonly IMediator _mediator;
    //    public GetProductListHandler(IProductAppService productAppService, IMediator mediator)
    //    {
    //        _productAppService = productAppService;
    //        _mediator = mediator;
    //    }

    //    public async Task<PagedResultDto<ProductCompositionDto>> Handle(HandleGetProductListEto request, CancellationToken cancellationToken)
    //    {
    //        var input = new ProductListFilterDto();
    //        input.Name = request.Name;
    //        input.SkipCount = request.SkipCount;
    //        input.MaxResultCount = request.MaxResultCount;

    //        var productResult = await _productAppService.GetListAsync(input);

    //        var initialProducts = ObjectMapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductCompositionDto>>(productResult.Items);
    //        var productList = initialProducts.ToList();
    //        await _mediator.Publish(new SubscribeGetProductListEto() { Products = productList });
    //        return new PagedResultDto<ProductCompositionDto>(productResult.TotalCount, productList);
    //    }
    //}
}
