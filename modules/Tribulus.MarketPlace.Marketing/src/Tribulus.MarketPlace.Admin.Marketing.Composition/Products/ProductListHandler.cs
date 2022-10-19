using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Events;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class ProductListHandler : INotificationHandler<ProductListEto>
    {
        private readonly IProductAppService _productAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IMediator _mediator;
        public ProductListHandler(IProductAppService productAppService,
            IObjectMapper objectMapper,
            IMediator mediator)
        {
            _productAppService = productAppService;
            _objectMapper = objectMapper;
            _mediator = mediator;
        }

        public async Task Handle(ProductListEto notification, CancellationToken cancellationToken)
        {
            //create input for paged result from marketing
            var productListInput = new ProductListFilterDto();
            productListInput.Name = notification.Filter.Name;
            productListInput.SkipCount = notification.Filter.SkipCount;
            productListInput.MaxResultCount = notification.Filter.MaxResultCount;

            var productsResult = await _productAppService.GetListAsync(productListInput);

            //create composition objects only for marketing products
            List<ProductCompositionDto> productCompositions = new List<ProductCompositionDto>();
            foreach (var product in productsResult.Items)
            {
                var productComposition = _objectMapper.Map<ProductDto, ProductCompositionDto>(product);
                productCompositions.Add(productComposition);
            }

            //send composition to the contributers, so they can compose the properties that is missing
            await _mediator.Publish(new ProductListSub()
            {
                Products = productCompositions
            });
            //update the notification products,so gateway can return this to the user.
            notification.Products = new PagedResultDto<ProductCompositionDto>(productsResult.TotalCount, productCompositions);


        }
    }
}
