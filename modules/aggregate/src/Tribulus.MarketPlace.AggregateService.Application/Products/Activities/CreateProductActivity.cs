using MassTransit;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Marketing.Products;
using Volo.Abp.ObjectMapping;

namespace Tribulus.MarketPlace.AggregateService.Products.Activities
{
    public class CreateProductActivity :
        ICreateProductActivity
    {
        private readonly IProductAppService _productAppService;
        private readonly IObjectMapper _objectMapper;
        public CreateProductActivity(IProductAppService productAppService, IObjectMapper objectMapper)
        {
            _productAppService = productAppService;
            _objectMapper = objectMapper;
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ProductLog> context)
        {
            await _productAppService.DeleteAsync(context.Log.ProductId);
            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ProductArguments> context)
        {
            var id = context.Arguments.ProductId;
            var userId = context.Arguments.UserId;
            var name = context.Arguments.Name;
            var description = context.Arguments.Description;

            var createProductDto = new CreateProductDto
            {
                Name = name,
                Description = description

            };
            var productDto = await _productAppService.CreateAsync(id, createProductDto);


            return context.CompletedWithVariables<ProductLog>(new { ProductId= id }, new { Product=productDto });
        }
    }
}
