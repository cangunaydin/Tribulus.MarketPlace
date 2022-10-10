using Tribulus.MarketPlace.Admin.Sales.Products;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class GetProductConsumer :AdminSalesCompositionService
    {
        private readonly IProductPriceAppService _productPriceAppService;
        public GetProductConsumer(IProductPriceAppService productPriceAppService)
        {
            _productPriceAppService = productPriceAppService;
        }

        //public async Task Consume(ConsumeContext<HandleGetProductEto> context)
        //{
        //    var id = context.Message.Product.Id;
        //    var productPrice = await _productPriceAppService.GetAsync(id);
        //    var productDto = ObjectMapper.Map<ProductPriceDto, ProductCompositionDto>(productPrice);
        //    await context.RespondAsync(productDto);
        //}
    }
}
