using MassTransit.Courier;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Marketing.Products;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class CreateProductActivity :
        ICreateProductActivity
    {
        private readonly IProductRepository _productRepository;
        private readonly IObjectMapper _objectMapper;
        public CreateProductActivity(IProductRepository productRepository, IObjectMapper objectMapper)
        {
            _productRepository = productRepository;
            _objectMapper = objectMapper;
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ProductLog> context)
        {
            await _productRepository.DeleteAsync(context.Log.ProductId);
            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ProductArguments> context)
        {
            var id = context.Arguments.ProductId;
            var userId = context.Arguments.UserId;
            var name = context.Arguments.Name;
            var description = context.Arguments.Description;

            var product = new Product(id, userId, name);
            product.Description = description;
            await _productRepository.InsertAsync(product, true);
            var productDto=_objectMapper.Map<Product, ProductDto>(product);


            return context.CompletedWithVariables<ProductLog>(new { ProductId= product.Id }, new { Product=productDto });
        }
    }
}
