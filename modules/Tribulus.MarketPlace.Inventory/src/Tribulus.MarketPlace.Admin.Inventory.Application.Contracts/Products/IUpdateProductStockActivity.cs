using MassTransit;

namespace Tribulus.MarketPlace.Admin.Inventory.Products
{
    public interface IUpdateProductStockActivity : IActivity<ProductStockArguments, ProductStockLog>
    {
    }

}
