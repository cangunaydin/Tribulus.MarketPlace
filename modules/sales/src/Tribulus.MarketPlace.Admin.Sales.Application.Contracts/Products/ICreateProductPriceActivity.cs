

using MassTransit;

namespace Tribulus.MarketPlace.Admin.Sales.Products;

public interface ICreateProductPriceActivity:
    IActivity<ProductPriceArguments, ProductPriceLog>
{
}
