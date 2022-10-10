using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Products;
using Tribulus.MarketPlace.Admin.Products.Handlers;
using Tribulus.MarketPlace.Marketing.Products;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class GetProductConsumer :AdminMarketingCompositionService
    {
        private readonly IProductAppService _productAppService;
        public GetProductConsumer(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        //public async Task Consume(ConsumeContext<HandleGetProductEto> context)
        //{
        //    var id = context.Message.Product.Id;
        //    var product = await _productAppService.GetAsync(id);
        //    var productDto= ObjectMapper.Map<ProductDto, ProductCompositionDto>(product);
        //    await context.RespondAsync(productDto);
        //}

    }
}
