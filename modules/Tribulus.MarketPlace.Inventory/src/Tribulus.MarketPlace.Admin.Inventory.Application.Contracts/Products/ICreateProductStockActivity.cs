using MassTransit;

namespace Tribulus.MarketPlace.Admin.Inventory.Products;

public interface ICreateProductStockActivity: IActivity<ProductStockArguments, ProductStockLog>
{
}
