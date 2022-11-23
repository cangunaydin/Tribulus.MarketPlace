using MassTransit;

namespace Tribulus.MarketPlace.Admin.Marketing.Products;

public interface ICreateProductActivity: IActivity<ProductArguments, ProductLog>
{
}
