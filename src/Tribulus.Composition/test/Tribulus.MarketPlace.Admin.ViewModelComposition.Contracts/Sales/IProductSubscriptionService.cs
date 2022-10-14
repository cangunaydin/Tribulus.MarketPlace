using Tribulus.MarketPlace.Admin.Products;

namespace Tribulus.MarketPlace.Admin.ViewModelComposition.Contracts.Sales
{
    public interface IProductSubscriptionService
    {
        void GetProducts(ProductFilterDto input);
    }
}
